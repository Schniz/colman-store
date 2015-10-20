using StoreForColman.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoreForColman.Controllers
{
    public class OrderController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        private Dictionary<int, int> CurrentOrder
        {
            get
            {
                if (Session["currentOrder"] == null)
                {
                    Session["currentOrder"] = new Dictionary<int, int>();
                }
                return Session["currentOrder"] as Dictionary<int, int>;
            }
        }

        private IEnumerable<dynamic> ProductsInCurrentOrder
        {
            get
            {
                int[] keys = CurrentOrder.Select(e => e.Key).ToArray();
                return from key in keys
                       join product in db.Products on key equals product.ID
                       select new
                       {
                           ID = product.ID,
                           Quantity = CurrentOrder[product.ID],
                           Name = product.Name,
                           PriceInNIS = product.PriceInNIS,
                           ManufactorName = product.ManufactorName
                       };
            }
        }

        private int ItemCount
        {
            get
            {
                return CurrentOrder.Values.Count();
            }
        }
        
        // GET: Order
        public ActionResult Index()
        {
            ViewBag.ItemCount = ItemCount;
            return View();
        }

        public ActionResult Cart()
        {
            return Json(ProductsInCurrentOrder.ToList(), JsonRequestBehavior.AllowGet);
        }
        
        [HttpPost]
        public ActionResult Add(int id)
        {
            try
            {
                Product product = db.Products.First(p => p.ID == id);
                if (!CurrentOrder.ContainsKey(id))
                {
                    CurrentOrder[id] = 1;
                }
                else
                {
                    CurrentOrder[id] = CurrentOrder[id] + 1;
                }
                return RedirectToAction("Cart");
            }
            catch
            {
                return Json(new { error = "An Error Occured." });
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                Product product = db.Products.First(p => p.ID == id);
                CurrentOrder.Remove(id);
                return RedirectToAction("Cart");
            }
            catch
            {
                return Json(new { error = "An Error Occured." });
            }
        }

        [HttpPost]
        public ActionResult Create()
        {
            try
            {
                var products = from product in ProductsInCurrentOrder
                               join p1 in db.Products on product.ID equals p1.ID
                               select new OrderedProduct
                               {
                                   CurrentPriceInNIS = product.PriceInNIS,
                                   Product = p1,
                                   Quantity = product.Quantity,
                               };
                if (products.Count() < 1) throw new Exception("אנא בחר מוצר כלשהו");
                String userId = (Session["user"] as ApplicationUser).Id;
                var userQuery = from u in db.Users
                                       where u.Id == userId
                                       select u;
                ApplicationUser user = userQuery.First();
                db.Orders.Add(new Order
                {
                    CreatedAt = DateTime.Now,
                    User = user,
                    Products = products.ToList()
                });
                db.SaveChanges();
                CurrentOrder.Clear();
                return Json(new { success = "yes" });
            }
            catch (Exception e)
            {
                return Json(new { error = e.Message });
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection form)
        {
            try
            {
                int quantity = int.Parse(form["Quantity"]);
                if (quantity < 0) throw new Exception();
                Product product = db.Products.First(p => p.ID == id);
                CurrentOrder[id] = quantity;
                return RedirectToAction("Cart");
            }
            catch
            {
                return Json(new { error = "An Error Occured." });
            }
        }
    }
}

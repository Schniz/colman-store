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
        
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Cart()
        {
            int[] keys = CurrentOrder.Select(e => e.Key).ToArray();
            var productsFromOrder = from key in keys
                                    join product in db.Products on key equals product.ID
                                    select new
                                    {
                                        ID = product.ID,
                                        Quantity = CurrentOrder[product.ID],
                                        Name = product.Name,
                                        ManufactorName = product.ManufactorName
                                    };
            return Json(productsFromOrder.ToList(), JsonRequestBehavior.AllowGet);
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
                } else
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
    }
}

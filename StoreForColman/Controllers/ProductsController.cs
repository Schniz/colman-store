using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StoreForColman.Models;

namespace StoreForColman.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Products
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }
        
        public ActionResult List()
        {
            string manufactor = Request.QueryString["manufactor"];
            string available = Request.QueryString["available"];
            string minPriceString = Request.QueryString["minPrice"];
            string maxPriceString = Request.QueryString["maxPrice"];
            double? minPrice = null;
            double? maxPrice = null;
            try
            {
                if (minPriceString != null) minPrice = double.Parse(minPriceString);
                if (maxPriceString != null) maxPrice = double.Parse(maxPriceString);
            }
            catch
            {
                return Json(new { Error = "Cannot convert minPrice or maxPrice" });
            }

            var products = db.Products.Select(product => product);
            if (manufactor != null)
            {
                products = from p in products where (p.ManufactorName.ToLower().Equals(manufactor.ToLower())) select p;
            }
            if (available != null)
            {
                products = from p in products where (p.AmountInStore > 0) select p;
            }
            if (minPrice.HasValue)
            {
                products = from p in products where (p.PriceInNIS >= minPrice.Value) select p;
            }
            if (maxPrice.HasValue)
            {
                products = from p in products where (p.PriceInNIS <= maxPrice.Value) select p;
            }
            
            return Json(products.ToList(), JsonRequestBehavior.AllowGet);
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,PriceInNIS,ManufactorName,AmountInStore")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,PriceInNIS,ManufactorName,AmountInStore")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StoreForColman.Models;
using System.Web.Helpers;

namespace StoreForColman.Controllers
{
    public class UsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FullName,PasswordHash,UserName")] ApplicationUser applicationUser)
        {
            applicationUser.HashPassword().IsAdmin = false;
            try
            {
                db.Users.Add(applicationUser);
                db.SaveChanges();
                Session["CreationSucceed"] = true;
                return RedirectToAction("Success");
            }
            catch
            {
                return RedirectToAction("Create");
            }
        }

        public ActionResult Success()
        {
            if (Session["CreationSucceed"] == null || !(bool)Session["CreationSucceed"])
            {
                return RedirectToAction("Create");
            }

            Session["CreationSucceed"] = false;
            return View();
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

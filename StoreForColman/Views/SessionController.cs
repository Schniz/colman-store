using StoreForColman.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoreForColman.Views
{
    public class SessionController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        
        // GET: Session/Create
        public ActionResult Create()
        {
            ViewBag.ErrorMessage = Session["ErrorMessage"];
            Session["ErrorMessage"] = null;
            return View();
        }

        // POST: Session/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                String username = Request.Form["username"];
                String pass = Request.Form["pass"];
                // TODO: Add insert logic here
                ApplicationUser appUser = db.Users.First(user => user.UserName.Equals(username) && user.PasswordHash.Equals(pass));
                Session["user"] = appUser;
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                Session["ErrorMessage"] = "שם המשתמש או הסיסמה לא נכונים";
                return RedirectToAction("Create");
            }
        }
    }
}

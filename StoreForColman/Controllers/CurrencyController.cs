using StoreForColman.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoreForColman.Controllers
{
    public class CurrencyController : Controller
    {
        // GET: Currency
        public ActionResult Index()
        {
            return Json(CurrencyConversionService.getData(), JsonRequestBehavior.AllowGet);
        }
    }
}
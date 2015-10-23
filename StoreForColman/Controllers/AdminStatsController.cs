using StoreForColman.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using StoreForColman.Filters;

namespace StoreForColman.Controllers
{
    public class Stats
    {
        public int Count { get; set; }
        public String Key { get; set; }
    }

    public class ViewStats
    {
        public List<Stats> Monthly { get; set; }
        public List<Stats> Users { get; set; }
    }

    [AdminFilter]
    public class AdminStatsController : Controller
    {
        private IQueryable<Stats> getStatsFor(IQueryable<IGrouping<string, Order>> query)
        {
            return query.Select(y => new Stats {
                Key = y.Key,
                Count = y.Count()
            }).Select(e => e);
        }

        ApplicationDbContext db = new ApplicationDbContext();
        // GET: AdminStats
        public ActionResult Index()
        {
            var orders = db.Orders.Include("User").Include("Products").Include(u => u.Products.Select(y => y.Product));
            var ordersByUser = orders.GroupBy(order => order.User.Id);
            var ordersByMonth = orders.GroupBy(order => order.CreatedAt.Month.ToString());
            var stats = new ViewStats
            {
                Monthly = getStatsFor(ordersByMonth).ToList(),
                Users = getStatsFor(ordersByUser).ToList()
            };
            return View(stats);
        }
    }
}
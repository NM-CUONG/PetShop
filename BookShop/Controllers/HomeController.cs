using BookShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookShop.Controllers
{
    public class HomeController : Controller
    {
        private ShopContext db = new ShopContext();

        //Trang chủ admin
        public ActionResult Admin()
        {
            return View();
        }


        //Menu con
        [ChildActionOnly]
        public PartialViewResult SubMenu()
        {
            var query = db.tb_Category.ToList();
            return PartialView(query);
        }
        public ActionResult Index()
        {
            var moive1 = db.tb_Product.OrderByDescending(x => x.id).Take(4).ToList();
            var moive2 = db.tb_Product.OrderByDescending(x => x.id).Skip(4).Take(4).ToList();

            ViewBag.moive1 = moive1;
            ViewBag.moive2 = moive2;

            var hot1 = db.tb_Product.OrderByDescending(x => x.price_new).Take(4).ToList();
            var hot2 = db.tb_Product.OrderByDescending(x => x.price_new).Skip(4).Take(4).ToList();

            ViewBag.hot1 = hot1;
            ViewBag.hot2 = hot2;

            return View();
        }
        
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
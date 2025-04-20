using Badminton.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Badminton.Controllers
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
            var vot = db.tb_Product.Where(p => p.category_id == 1).OrderBy(p => p.price_new).Take(4).ToList();
            ViewData["Vot"] = vot;

            var giay = db.tb_Product.Where(p => p.category_id == 2).OrderBy(p => p.price_new).Take(4).ToList();
            ViewData["Giay"] = giay;

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
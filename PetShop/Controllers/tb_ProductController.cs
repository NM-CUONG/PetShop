using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Badminton.Models;
using Microsoft.Ajax.Utilities;

namespace Badminton.Controllers
{
    public class tb_ProductController : Controller
    {
        private ShopContext db = new ShopContext();
        
        //Xem toàn bộ sản phẩm

        public ActionResult ShowAllProduct()
        {
            var allProduct = db.tb_Product.ToList();
            return View(allProduct);
        }

        // Tìm kiếm
        public ActionResult Search(string searchString)
        {
            var listSearched  = db.tb_Product.Where(p => p.title.Contains(searchString)).ToList();
            ViewBag.SearchString = searchString;
            return View(listSearched);
        }
        //Kiểm tra sản phẩm có còn trong kho không
        public ActionResult checkSize(string ProductName, string Size)
        {
            tb_Product sp = db.tb_Product.SingleOrDefault(p => p.title == ProductName && p.size == Size);
            if (sp != null)
            {
                var respon = new { status = "yes"};
                return Json(respon, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var respon = new { status = "no"};
                return Json(respon, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult fill(int id, int sort, int cate)
        {
            // Chọn brand rồi mới sắp xếp
            if (id != 0)
            {
                if (sort == 0)
                {
                    return PartialView(db.tb_Product.Where(p => p.brand_id == id && p.category_id == cate).ToList());
                }
                if (sort == 1)
                {
                    return PartialView(db.tb_Product.Where(p => p.brand_id == id && p.category_id == cate).OrderBy(p => p.title).ToList());
                }
                if (sort == 2)
                {
                    return PartialView(db.tb_Product.Where(p => p.brand_id == id && p.category_id == cate).OrderByDescending(p => p.title).ToList());
                }
                if (sort == 3)
                {
                    return PartialView(db.tb_Product.Where(p => p.brand_id == id && p.category_id == cate).OrderBy(p => p.price_new).ToList());
                }
                if (sort == 4)
                {
                    return PartialView(db.tb_Product.Where(p => p.brand_id == id && p.category_id == cate).OrderByDescending(p => p.price_new).ToList());
                }
            }

            // Chưa chọn brand nhưng vẫn muốn sắp xếp
            if (id == 0)
            {
                if (sort == 0)
                {
                    return PartialView(db.tb_Product.Where(p => p.category_id == cate).ToList());
                }
                if (sort == 1)
                {
                    return PartialView(db.tb_Product.Where(p => p.category_id == cate).OrderBy(p => p.title).ToList());
                }
                if (sort == 2)
                {
                    return PartialView(db.tb_Product.Where(p => p.category_id == cate).OrderByDescending(p => p.title).ToList());
                }
                if (sort == 3)
                {
                    return PartialView(db.tb_Product.Where(p => p.category_id == cate).OrderBy(p => p.price_new).ToList());
                }
                if (sort == 4)
                {
                    return PartialView(db.tb_Product.Where(p => p.category_id == cate).OrderByDescending(p => p.price_new).ToList());
                }
            }
            
            return PartialView(db.tb_Product.ToList());
        }

        // GET: Product detail  
        public  ActionResult ProductDetail(int id)
        {
            tb_Product query = db.tb_Product.SingleOrDefault(p => p.id == id);
            return View(query);
        }

        //[Route ("productsByCateID/CateID/{CateID?}")]

        // GET: Product by CateID
        public ActionResult ProductsByCateID(int id)
        {
            string cateName = db.tb_Category.SingleOrDefault(p => p.id == id).categoryname;
            ViewData["cateName"] = cateName;
            ViewData["cateID"] = id;

            ViewBag.lstBrand = db.tb_Brand.ToList();

            var list = db.tb_Product.Where(p => p.category_id == id).ToList();

            return View(list);
        }

        // GET: tb_Product
        public ActionResult Index()
        {
            var tb_Product = db.tb_Product.Include(t => t.tb_Brand).Include(t => t.tb_Category);
            return View(tb_Product.ToList());
        }

        // GET: tb_Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_Product tb_Product = db.tb_Product.Find(id);
            if (tb_Product == null)
            {
                return HttpNotFound();
            }
            return View(tb_Product);
        }

        // GET: tb_Product/Create
        public ActionResult Create()
        {
            ViewBag.brand_id = new SelectList(db.tb_Brand, "id", "brandname");
            ViewBag.category_id = new SelectList(db.tb_Category, "id", "categoryname");
            return View();
        }

        // POST: tb_Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,title,price,price_new,img,descript,size,stt,category_id,brand_id")] tb_Product tb_Product)
        {
            if (ModelState.IsValid)
            {
                tb_Product.img = "";
                var f = Request.Files["ufile"];
                if (f!=null && f.ContentLength>0)
                {
                    string image = System.IO.Path.GetFileName(f.FileName);
                    tb_Product.img = image;
                    string path = Server.MapPath("/Images/" + image);
                    f.SaveAs(path);
                }
                db.tb_Product.Add(tb_Product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.brand_id = new SelectList(db.tb_Brand, "id", "brandname", tb_Product.brand_id);
            ViewBag.category_id = new SelectList(db.tb_Category, "id", "categoryname", tb_Product.category_id);
            return View(tb_Product);
        }

        // GET: tb_Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_Product tb_Product = db.tb_Product.Find(id);
            if (tb_Product == null)
            {
                return HttpNotFound();
            }
            ViewBag.brand_id = new SelectList(db.tb_Brand, "id", "brandname", tb_Product.brand_id);
            ViewBag.category_id = new SelectList(db.tb_Category, "id", "categoryname", tb_Product.category_id);
            return View(tb_Product);
        }

        // POST: tb_Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,title,price,price_new,img,descript,size,stt,category_id,brand_id")] tb_Product tb_Product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_Product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.brand_id = new SelectList(db.tb_Brand, "id", "brandname", tb_Product.brand_id);
            ViewBag.category_id = new SelectList(db.tb_Category, "id", "categoryname", tb_Product.category_id);
            return View(tb_Product);
        }

        // GET: tb_Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_Product tb_Product = db.tb_Product.Find(id);
            if (tb_Product == null)
            {
                return HttpNotFound();
            }
            return View(tb_Product);
        }

        // POST: tb_Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tb_Product tb_Product = db.tb_Product.Find(id);
            db.tb_Product.Remove(tb_Product);
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

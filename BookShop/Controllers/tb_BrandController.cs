using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookShop.Models;

namespace BookShop.Controllers
{
    public class tb_BrandController : Controller
    {
        private ShopContext db = new ShopContext();

        // GET: tb_Brand
        public ActionResult Index()
        {
            return View(db.tb_Brand.ToList());
        }

        // GET: tb_Brand/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_Brand tb_Brand = db.tb_Brand.Find(id);
            if (tb_Brand == null)
            {
                return HttpNotFound();
            }
            return View(tb_Brand);
        }

        // GET: tb_Brand/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tb_Brand/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,brandname,logo")] tb_Brand tb_Brand)
        {
            if (ModelState.IsValid)
            {
                db.tb_Brand.Add(tb_Brand);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tb_Brand);
        }

        // GET: tb_Brand/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_Brand tb_Brand = db.tb_Brand.Find(id);
            if (tb_Brand == null)
            {
                return HttpNotFound();
            }
            return View(tb_Brand);
        }

        // POST: tb_Brand/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,brandname,logo")] tb_Brand tb_Brand)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_Brand).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tb_Brand);
        }

        // GET: tb_Brand/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_Brand tb_Brand = db.tb_Brand.Find(id);
            if (tb_Brand == null)
            {
                return HttpNotFound();
            }
            return View(tb_Brand);
        }

        // POST: tb_Brand/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tb_Brand tb_Brand = db.tb_Brand.Find(id);
            db.tb_Brand.Remove(tb_Brand);
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

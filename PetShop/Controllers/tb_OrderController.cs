using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Badminton.Models;

namespace Badminton.Controllers
{
    public class tb_OrderController : Controller
    {
        private ShopContext db = new ShopContext();

        //Xem danh sách đơn hàng

        public ActionResult ShowListOrder(int? id)
        {
            var list = db.tb_Order.Where(p => p.userid == id).ToList();
            return View(list);
        }

        // GET: tb_Order
        public ActionResult Index()
        {
            var tb_Order = db.tb_Order.Include(t => t.tb_User);
            return View(tb_Order.ToList());
        }

        // GET: tb_Order/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_Order tb_Order = db.tb_Order.Find(id);
            if (tb_Order == null)
            {
                return HttpNotFound();
            }
            return View(tb_Order);
        }

        // GET: tb_Order/Create
        public ActionResult Create()
        {
            ViewBag.userid = new SelectList(db.tb_User, "id", "fullname");
            return View();
        }

        // POST: tb_Order/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,fullname,sdt,addr,note,orderdate,stt,userid")] tb_Order tb_Order)
        {
            if (ModelState.IsValid)
            {
                db.tb_Order.Add(tb_Order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.userid = new SelectList(db.tb_User, "id", "fullname", tb_Order.userid);
            return View(tb_Order);
        }

        // GET: tb_Order/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_Order tb_Order = db.tb_Order.Find(id);
            if (tb_Order == null)
            {
                return HttpNotFound();
            }
            ViewBag.userid = new SelectList(db.tb_User, "id", "fullname", tb_Order.userid);
            return View(tb_Order);
        }

        // POST: tb_Order/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,fullname,sdt,addr,note,orderdate,stt,userid")] tb_Order tb_Order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_Order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.userid = new SelectList(db.tb_User, "id", "fullname", tb_Order.userid);
            return View(tb_Order);
        }

        // GET: tb_Order/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_Order tb_Order = db.tb_Order.Find(id);
            if (tb_Order == null)
            {
                return HttpNotFound();
            }
            return View(tb_Order);
        }

        // POST: tb_Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tb_Order tb_Order = db.tb_Order.Find(id);
            db.tb_Order.Remove(tb_Order);
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

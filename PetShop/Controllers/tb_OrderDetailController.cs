using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.DynamicData;
using System.Web.Mvc;
using System.Web.Services.Description;
using Badminton.Models;

namespace Badminton.Controllers
{
    public class tb_OrderDetailController : Controller
    {
        private ShopContext db = new ShopContext();

        //GET OrderDetail
        public ActionResult ShowOrderDetail(int id)
        {
            var query = db.tb_Order.SingleOrDefault(p => p.id == id);
            return View(query);
        }
        public ActionResult SaveOrder(FormCollection form)
        {
            //Login, mới cho đặt hàng
            if (Session["account"] == null)
            {
                return Redirect("/tb_User/Login");
            }

            //lấy dữ liệu gửi từ form xác nhận đặt hàng
            int userID = (int)Session["account"];
            string fullname = form["name"];
            string SDT = form["phone_Number"];
            string Addr = form["address"];
            DateTime OrderDate = DateTime.Now;

            // Lưu đơn hàng vào bảng order
            tb_Order order = new tb_Order();
            order.fullname = fullname;
            order.sdt = SDT;
            order.addr = Addr;
            order.orderdate = OrderDate;
            order.userid = userID;
            order.stt = "Chưa xác nhận";
            db.tb_Order.Add(order);
            db.SaveChanges();

            // lưu chi tiết đơn hàng vào bảng orderdetail
            List<ProductSelected> list = (List<ProductSelected>)Session["cart"];
            foreach (var item in list)
            {
                tb_OrderDetail orderDetail = new tb_OrderDetail();
                orderDetail.order_id = order.id;
                orderDetail.product_id = item.productID;
                orderDetail.num = item.productQuantity;
                db.tb_OrderDetail.Add(orderDetail);
                db.SaveChanges();
            }
            //đặt hàng xong thì xóa giỏ hàng
            Session.Remove("cart");

            return RedirectToAction("ShowOrderDetail", new {id = order.id});
        }

        // GET: tb_OrderDetail
        public ActionResult Index()
        {
            var tb_OrderDetail = db.tb_OrderDetail.Include(t => t.tb_Order).Include(t => t.tb_Product);
            return View(tb_OrderDetail.ToList());
        }

        // GET: tb_OrderDetail/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_OrderDetail tb_OrderDetail = db.tb_OrderDetail.Find(id);
            if (tb_OrderDetail == null)
            {
                return HttpNotFound();
            }
            return View(tb_OrderDetail);
        }

        // GET: tb_OrderDetail/Create
        public ActionResult Create()
        {
            ViewBag.order_id = new SelectList(db.tb_Order, "id", "fullname");
            ViewBag.product_id = new SelectList(db.tb_Product, "id", "title");
            return View();
        }

        // POST: tb_OrderDetail/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,order_id,product_id,num")] tb_OrderDetail tb_OrderDetail)
        {
            if (ModelState.IsValid)
            {
                db.tb_OrderDetail.Add(tb_OrderDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.order_id = new SelectList(db.tb_Order, "id", "fullname", tb_OrderDetail.order_id);
            ViewBag.product_id = new SelectList(db.tb_Product, "id", "title", tb_OrderDetail.product_id);
            return View(tb_OrderDetail);
        }

        // GET: tb_OrderDetail/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_OrderDetail tb_OrderDetail = db.tb_OrderDetail.Find(id);
            if (tb_OrderDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.order_id = new SelectList(db.tb_Order, "id", "fullname", tb_OrderDetail.order_id);
            ViewBag.product_id = new SelectList(db.tb_Product, "id", "title", tb_OrderDetail.product_id);
            return View(tb_OrderDetail);
        }

        // POST: tb_OrderDetail/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,order_id,product_id,num")] tb_OrderDetail tb_OrderDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_OrderDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.order_id = new SelectList(db.tb_Order, "id", "fullname", tb_OrderDetail.order_id);
            ViewBag.product_id = new SelectList(db.tb_Product, "id", "title", tb_OrderDetail.product_id);
            return View(tb_OrderDetail);
        }

        // GET: tb_OrderDetail/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_OrderDetail tb_OrderDetail = db.tb_OrderDetail.Find(id);
            if (tb_OrderDetail == null)
            {
                return HttpNotFound();
            }
            return View(tb_OrderDetail);
        }

        // POST: tb_OrderDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tb_OrderDetail tb_OrderDetail = db.tb_OrderDetail.Find(id);
            db.tb_OrderDetail.Remove(tb_OrderDetail);
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

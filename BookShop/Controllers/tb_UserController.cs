using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using BookShop.Models;

namespace BookShop.Controllers
{
    public class tb_UserController : Controller
    {
        private ShopContext db = new ShopContext();

        //DELETE CART
        public ActionResult DeleteCart(int id)
        {
            List<ProductSelected> list = (List<ProductSelected>)Session["cart"];

            ProductSelected productDelete = list.FirstOrDefault(p => p.id == id);
            list.Remove(productDelete);
            Session["cart"] = list;
            return View("Cart");
        }

        //Buy 
        public ActionResult Buy()
        {
            return View();
        }

        //GET: Cart

        public ActionResult Cart()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Confirm(int? productquantity, long productId)
        {
            var product = db.tb_Product.FirstOrDefault(x => x.id == productId);
            var productSelected = new ProductSelected();
            if (product != null)
            {
                productSelected.productID = product.id;
                productSelected.productName = product.title;
                productSelected.productQuantity = productquantity ?? 0;
                productSelected.productPrice = product.price_new != null ? (float)product.price_new : 0;
                productSelected.productImage = product.img;
            }

            List<ProductSelected> listProduct = Session["cart"] as List<ProductSelected> ?? new List<ProductSelected>();

            if (listProduct.Contains(productSelected))
            {
                int index = listProduct.IndexOf(productSelected);
                listProduct[index].productQuantity += productSelected.productQuantity;
            }
            else
            {
                int id = listProduct.Count;
                product.id = id + 1;
                listProduct.Add(productSelected);
            }

            Session["cart"] = listProduct;

            return new HttpStatusCodeResult(200);
        }

        // GET: tb_User
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult XLogin(string username, string password)
        {
            var query = db.tb_User.SingleOrDefault(p => p.username == username && p.passwd == password);
            if (query == null)
            {
                ViewBag.msg = "Tài khoản không tồn tại";
                return View("Login");
            }

            Session["account"] = query.id;
            Session["UserName"] = query.fullname;

            if (query.role_id == 1)
            {
                return Redirect("/tb_Product/Index");
            }
            return Redirect("/Home/Index");
        }
        public ActionResult Logout()
        {
            Session.Remove("account");
            Session.Remove("UserName");
            return Redirect("/Home/Index");
        }
        public ActionResult Regis()
        {
            return View();
        }
        public ActionResult Regising(string name, string email, string phone_number, string password, string repassword)
        {
            if (password != repassword)
            {
                ViewBag.msg = "Mật khẩu không khớp!";
                return View("Regis");
            }
            try
            {
                tb_User newUser = new tb_User();
                newUser.fullname = name;
                newUser.username = email;
                newUser.phonenumber = phone_number;
                newUser.passwd = password;
                newUser.role_id = 2;

                db.tb_User.Add(newUser);
                db.SaveChanges();
                return Redirect("/Home/Index");
            }
            catch (Exception)
            {
                ViewBag.msg = "Email đã tồn tại!";
                return View("Regis");
            }
            
        }
        public ActionResult Index()
        {
            var tb_User = db.tb_User.Include(t => t.tb_Role);
            return View(tb_User.ToList());
        }

        // GET: tb_User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_User tb_User = db.tb_User.Find(id);
            if (tb_User == null)
            {
                return HttpNotFound();
            }
            return View(tb_User);
        }

        // GET: tb_User/Create
        public ActionResult Create()
        {
            ViewBag.role_id = new SelectList(db.tb_Role, "id", "rolename");
            return View();
        }

        // POST: tb_User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,fullname,username,phonenumber,addr,passwd,role_id")] tb_User tb_User)
        {
            if (ModelState.IsValid)
            {
                db.tb_User.Add(tb_User);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.role_id = new SelectList(db.tb_Role, "id", "rolename", tb_User.role_id);
            return View(tb_User);
        }

        // GET: tb_User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_User tb_User = db.tb_User.Find(id);
            if (tb_User == null)
            {
                return HttpNotFound();
            }
            ViewBag.role_id = new SelectList(db.tb_Role, "id", "rolename", tb_User.role_id);
            return View(tb_User);
        }

        // POST: tb_User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,fullname,username,phonenumber,addr,passwd,role_id")] tb_User tb_User)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_User).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.role_id = new SelectList(db.tb_Role, "id", "rolename", tb_User.role_id);
            return View(tb_User);
        }

        // GET: tb_User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_User tb_User = db.tb_User.Find(id);
            if (tb_User == null)
            {
                return HttpNotFound();
            }
            return View(tb_User);
        }

        // POST: tb_User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tb_User tb_User = db.tb_User.Find(id);
            db.tb_User.Remove(tb_User);
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoFinalDeporte.Models;
using Microsoft.AspNet.Identity;

namespace ProyectoFinalDeporte.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Products
        public ActionResult Index(string sortOrder)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "price_desc" : "Price";

            var products = from s in db.Products
                           select s;

            switch (sortOrder)
            {
                case "name_desc":
                    products = products.OrderByDescending(s => s.Name);
                    break;
                case "price_desc":
                    products = products.OrderByDescending(s => s.Price);
                    break;
                case "Price":
                    products = products.OrderBy(s => s.Price);
                    break;
                default:
                    products = products.OrderBy(s => s.Name);
                    break;
            }

            return View(products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            string currentUserId = User.Identity.GetUserId();
            var userAdmin = db.Admins.Where(p => p.IdUser == currentUserId);

            if (userAdmin.Count() == 0)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        // POST: Products/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,Name,Description,ImageProduct,Price")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string currentUserId = User.Identity.GetUserId();
            var userAdmin = db.Admins.Where(p => p.IdUser == currentUserId);

            if (userAdmin.Count() == 0)
            {
                return RedirectToAction("Index");
            }

            Product product = db.Products.Find(id);

            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,Name,Description,ImageProduct,Price")] Product product)
        {
            string currentUserId = User.Identity.GetUserId();
            var userAdmin = db.Admins.Where(p => p.IdUser == currentUserId);

            if (userAdmin.Count() == 0)
            {
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string currentUserId = User.Identity.GetUserId();
            var userAdmin = db.Admins.Where(p => p.IdUser == currentUserId);

            if (userAdmin.Count() == 0)
            {
                return RedirectToAction("Index");
            }


            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            string currentUserId = User.Identity.GetUserId();
            var userAdmin = db.Admins.Where(p => p.IdUser == currentUserId);

            if (userAdmin.Count() == 0)
            {
                return RedirectToAction("Index");
            }
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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

        // GET: Products/Add/5
        public ActionResult Add(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string currentUserId = User.Identity.GetUserId();
            var userAdmin = db.Admins.Where(p => p.IdUser == currentUserId);

            Cart addToCart = new Cart();

            addToCart.ProductId = (int)id;
            addToCart.UserId = currentUserId;

            db.Carts.Add(addToCart);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}

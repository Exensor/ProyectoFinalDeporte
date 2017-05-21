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
    public class CartsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Carts
        public ActionResult Index()
        {
            string currentUserId = User.Identity.GetUserId();
            var userCarts = db.Carts.Where(p => p.UserId == currentUserId).ToList();

            return View(userCarts);
        }
       
        // GET: Carts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart cart = db.Carts.Find(id);
            string currentUserId = User.Identity.GetUserId();

            
            if ((cart.UserId != currentUserId) || (cart == null))
            {
                return HttpNotFound();
            }
            return View(cart);
        }

        // POST: Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cart cart = db.Carts.Find(id);
            

            db.Carts.Remove(cart);
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

        public ActionResult BuyAll()
        {
            string currentUserId = User.Identity.GetUserId();
            var query = db.Carts.Where(w => w.UserId == currentUserId);

            foreach (Cart element in query)
            {
                db.Carts.Remove(element);
               
            }
            db.SaveChanges();

            return RedirectToAction("Index");

        }
    }
}

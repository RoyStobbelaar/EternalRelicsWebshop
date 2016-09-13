using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ServerEternalRelics.DAL;
using ServerEternalRelics.Models;

namespace ServerEternalRelics.Controllers
{
    public class ProductController : Controller
    {
        private WebshopContext db = new WebshopContext();

        // GET: Product
        public ActionResult Index()
        {
            return View(db.Product.ToList());
        }

        // GET: Product/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
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

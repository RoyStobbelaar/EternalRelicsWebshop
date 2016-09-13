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
    public class CategoryController : Controller
    {
        private WebshopContext db = new WebshopContext();

        // GET: Category
        public ActionResult Index()
        {
            return View(db.Category.ToList());
        }

        // GET: Category/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Category.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
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

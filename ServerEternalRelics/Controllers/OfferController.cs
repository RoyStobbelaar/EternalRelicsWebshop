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
    public class OfferController : Controller
    {
        private WebshopContext db = new WebshopContext();

        // GET: Offer
        /* Return all offers */
        public ActionResult Index()
        {
            var offer = db.Offer.Include(o => o.Product);
            return View(offer.ToList());
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

using OptimedCorporation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OptimedCorporation.Controllers
{
    public class BrandsController : Controller
    {

        private OptimedCorporationContext db = new OptimedCorporationContext();
        // GET: Brands
        public ActionResult EtniaBarcelona()
        {
            string menu = "Brand - Etnia Barcelona";
            return View(db.Banners.Where(x => x.Menu == menu).ToList());
        }

        public ActionResult Silhouette()
        {
            string menu = "Brand - Silhouette";
            return View(db.Banners.Where(x => x.Menu == menu).ToList());
        }

        public ActionResult Adidas()
        {
            string menu = "Brand - Adidas";
            return View(db.Banners.Where(x => x.Menu == menu).ToList());
           
        }
    }
}

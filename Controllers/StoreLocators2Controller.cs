using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using OptimedCorporation.Models;


namespace OptimedCorporation.Controllers
{
    
    public class StoreLocators2Controller : Controller
    {
        private OptimedCorporationContext db = new OptimedCorporationContext();

        // GET: StoreLocators

        [NonAction]
        public string GetIpaddress()
        {
            string ipaddress;
            ipaddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ipaddress == "" || ipaddress == null)
                ipaddress = Request.ServerVariables["REMOTE_ADDR"];
            return ipaddress;
        }

        [NonAction]
        public void BindStateFromSL()
        {
            var stateFromSL = (from d in db.StoreLocators select d.State).Distinct();
            var state1 = db.States.ToList();

            var stateList = db.States.Where(f => stateFromSL.Contains(f.StateName)).ToList();
            ViewBag.StateList1 = stateList;
        }
        public List<State> BindState()
        {
            this.db.Configuration.ProxyCreationEnabled = false;

            List<State> lstState = new List<State>();
            try
            {
                lstState = db.States.ToList();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            ViewBag.StateList = lstState;            

            return lstState;
        }

        [HttpPost]

        public ActionResult GetCityList(string stateID)
        {
            List<City> lstcity = new List<City>();
            int stateiD = Convert.ToInt32(stateID);
           
            lstcity = (db.Cities.Where(x => x.StateId == stateiD)).ToList<City>();
            
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            string result = javaScriptSerializer.Serialize(lstcity);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult GetCityFromSL(string stateID)
        {
            ViewBag.StateError = null;

            var state = db.States.Find(int.Parse(stateID));
            var stName = state.StateName;

            var cityList = (from d in db.StoreLocators.Where(x => x.State == stName) select d.City).Distinct();

            List<City> lstcity = new List<City>();
            int stateiD = Convert.ToInt32(stateID);

            lstcity = (db.Cities.Where(x => x.StateId == stateiD)).ToList<City>();


            //var cityFromSL = (from d in db.StoreLocators.Where(x=> x.State==stateID) select d.City).Distinct();

            var cityList11 = lstcity.Where(f => cityList.Contains(f.CityName)).ToList();
           //var cityList11 = db.Cities.Where(f => cityList.Contains(f.CityName)).ToList();

            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            string result = javaScriptSerializer.Serialize(cityList11);
            return Json(result, JsonRequestBehavior.AllowGet);
        }



        //public List<City> BindCity(int stateId)
        //{
        //    List<City> lstCity = new List<City>();
        //    try
        //    {
        //        this.db.Configuration.ProxyCreationEnabled = false;

        //       lstCity = db.Cities.Where(x=>x.StateId == stateId).ToList();

        //    }
        //    catch (Exception ex)
        //    {
        //        ex.ToString();
        //    }
        //    return lstCity;
        //}

        public ActionResult BindCity(int stateId)
        {
            ViewBag.CityList = new SelectList(db.Cities.Where(x => x.StateId == stateId), "CityId", "CityName");
            return ViewBag.CityList;
        }

       
            


        [AllowAnonymous]
        public ActionResult Index()
        {
            BindStateFromSL();          

            string state, city, pin;

            if (TempData["State"] != null)
            {
                state = TempData["State"].ToString();
                Request.Form["state"] = state;
            }
            else
            { state = "Maharashtra"; }

            if (TempData["City"] != null)
            {
                city = TempData["City"].ToString();
            }
            else
            {
                city = "Mumbai";
            }

            if (TempData["State"] != null)
            {
                pin = TempData["Pin"].ToString();
            }
            
            //ViewBag.State = new SelectList(db.States, "StateId", "StateName");
            //ViewBag.City = new SelectList(db.Cities, "CityId", "CityName");
            ViewBag.AddressDetails = db.StoreLocators.Where(x => x.State == state && x.City == city).ToList();
            Location(state, city);
            return View();
        }

        public string Location(string state, string city)
        {
            //var result = db.StoreLocators.Where(x => x.State == sl.State && x.City == sl.City).ToList();
            //ViewBag.AddressDetails = result;

            string markers = "[";
            //var result = db.StoreLocators.ToList();
            var result = db.StoreLocators.Where(x => x.State == state && x.City == city).ToList();

            foreach (var i in result)
            {
                markers += "{";
                markers += string.Format("'title': '{0}',", i.City);
                markers += string.Format("'lat': '{0}',", i.Latitude);
                markers += string.Format("'lng': '{0}',", i.Longitude);
                markers += string.Format("'description': '{0}'", i.State);
                markers += "},";
            }

            markers += "];";
            ViewBag.Markers = markers;
            return ViewBag.Markers;

          //return View("Index");
        }

        public string Location(string pin)
        {   
            string markers = "[";
            //var result = db.StoreLocators.ToList();
            var result = db.StoreLocators.Where(x => x.Pin == pin).ToList();

            foreach (var i in result)
            {
                markers += "{";
                markers += string.Format("'title': '{0}',", i.City);
                markers += string.Format("'lat': '{0}',", i.Latitude);
                markers += string.Format("'lng': '{0}',", i.Longitude);
                markers += string.Format("'description': '{0}'", i.State);
                markers += "},";
            }

            markers += "];";
            ViewBag.Markers = markers;

            return ViewBag.Markers;

            //return View("Index");
        }

        //public ActionResult Location()
        //{
        //    string markers = "[";
        //    string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        //    using (SqlConnection con = new SqlConnection(CS))
        //    {
        //        SqlCommand cmd = new SqlCommand("spGetMap", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        con.Open();
        //        SqlDataReader sdr = cmd.ExecuteReader();
        //        while (sdr.Read())
        //        {
        //            markers += "{";
        //            markers += string.Format("'title': '{0}',", sdr["CityName"]);
        //            markers += string.Format("'lat': '{0}',", sdr["Latitude"]);
        //            markers += string.Format("'lng': '{0}',", sdr["Longitude"]);
        //            markers += string.Format("'description': '{0}'", sdr["Description"]);
        //            markers += "},";
        //        }
        //    }
        //    markers += "];";
        //    ViewBag.Markers = markers;
        //    return View();
        //} 

        public ActionResult ViewStoreLocator()
        {
            return View(db.StoreLocators.ToList());
        }

        // GET: StoreLocators/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StoreLocator storeLocator = db.StoreLocators.Find(id);
            if (storeLocator == null)
            {
                return HttpNotFound();
            }
            return View(storeLocator);
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(StoreLocator sl)
        {
            //if (ModelState.IsValid)
            //{
            try
            {
                string State = "";
                string City = "";

                if (sl.Pin == null && sl.State == "Select State")
                {
                    BindStateFromSL();

                   // TempData["StateError"] = "Please Select State";

                    ViewBag.StateError = "Please Select State"; // working

                    // ModelState.AddModelError(string.Empty, "Please Select State");  this is also working

                    return View("Index", sl);
                }

                if (sl.State != "Select State")
                {
                    var state = db.States.Find(int.Parse(sl.State));
                    State = state.StateName;
                }

                if (sl.City != "Select City")
                {
                    var city = db.Cities.Find(int.Parse(sl.City));
                    City = city.CityName;
                }

                if (sl.Pin == null && sl.City == null)
                {
                    BindStateFromSL();
                    ViewBag.CityError = "Please Select City";                    
                    return View("Index", sl);
                }

                if (sl.Pin != null)
                {
                    Location(sl.Pin);
                    var result = db.StoreLocators.Where(x => x.Pin == sl.Pin).ToList();
                    //if (result.Count == 0)
                    //{
                    //    ViewBag.NotFoundMsg = "No Address Found!";
                    //}
                    ViewBag.AddressDetails = result;
                    ModelState.Clear();
                }

                else if (sl.State != null && sl.City != null)
                {
                    Location(State, City);
                    var result = db.StoreLocators.Where(x => x.State == State && x.City == City).ToList();
                    //if(result.Count==0)
                    //{
                    //    ViewBag.NotFoundMsg = "No Address Found!";
                    //}
                    ViewBag.AddressDetails = result;
                    ModelState.Clear();
                }
            }

            catch { return RedirectToAction("Index"); }

            //}

            BindStateFromSL();
            //ModelState.Clear();
            return View("Index", sl);
        }        

        // GET: StoreLocators/Create
        public ActionResult Create()
        {
            ViewBag.StateList1 = db.States.ToList();
            ViewBag.StateList = new SelectList(db.States.ToList(), "StateId", "StateName");
            ViewBag.CityList = new SelectList(db.Cities.ToList(), "CityId", "CityName");            
            return View();
        }

        // POST: StoreLocators/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StoreLocator storeLocator)
        {
            if (ModelState.IsValid)
            {
                var state = db.States.Find(int.Parse(storeLocator.State));
                var city = db.Cities.Find(int.Parse(storeLocator.City));

                storeLocator.State = state.StateName;
                storeLocator.City = city.CityName;

                storeLocator.DateAdded = DateTime.Now;
                storeLocator.Ipaddress = GetIpaddress();

                db.StoreLocators.Add(storeLocator);
                db.SaveChanges();
                ModelState.Clear();
                ViewBag.StateList1 = db.States.ToList();
                ViewBag.Message = "Store Locator has been added uccessfully.";
                return View(new StoreLocator());
            }

            return View(storeLocator);
        }

        // GET: StoreLocators/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StoreLocator storeLocator = db.StoreLocators.Find(id);
            if (storeLocator == null)
            {
                return HttpNotFound();
            }
            return View(storeLocator);
        }

        // POST: StoreLocators/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StoreLocator storeLocator)
        {
            if (ModelState.IsValid)
            {
                db.Entry(storeLocator).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ViewStoreLocator");
            }
            return View(storeLocator);
        }

        // GET: StoreLocators/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StoreLocator storeLocator = db.StoreLocators.Find(id);
            if (storeLocator == null)
            {
                return HttpNotFound();
            }
            return View(storeLocator);
        }

        // POST: StoreLocators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StoreLocator storeLocator = db.StoreLocators.Find(id);
            db.StoreLocators.Remove(storeLocator);
            db.SaveChanges();
            return RedirectToAction("ViewStoreLocator");
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

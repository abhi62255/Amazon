using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Amazon.Controllers
{
    [Authorize(Users = "admin_123456")]
    public class AdminHomeController : Controller
    {
        XKARTEntities _db = new XKARTEntities();
        // GET: AdminHome
        public ActionResult Index()
        {
            ViewBag.countrydrop = _db.CITies.Select(x => new SelectListItem { Text = x.City1, Value = x.CityId.ToString() }).ToList();
            return View();
        }

        [HttpGet]
        public ActionResult AddAddress()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddAddress(ADDRESS model)
        {

            if (ModelState.IsValid)
            {
                _db.ADDRESSes.Add(model);
                _db.SaveChanges();
                Session["ID"] = Convert.ToInt32( model.AddressId);
                return RedirectToAction("AddSeller", "AdminHome");
            }
            return View(model);
        }


        [HttpGet]
        public ActionResult AddSeller()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddSeller(SELLER model)
        {

            if (ModelState.IsValid)
            {
                model.SellerAddressId = Convert.ToInt32( Session["ID"]);
                _db.SELLERs.Add(model);
                _db.SaveChanges();
                return RedirectToAction("Index", "AdminHome");
            }
            
            return View(model);
        }


    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
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
            return View();
        }


        //Add Seller
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



        //Add product
        [HttpGet]
        public ActionResult AddProduct()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddProduct(PRODUCT model)
        {

            if (ModelState.IsValid)
            {
                _db.PRODUCTs.Add(model);
                _db.SaveChanges();
                Session["ID"] = Convert.ToInt32(model.ProductId);
                return RedirectToAction("AddDescription", "AdminHome");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult AddDescription()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddDescription(PRODUCTDESCRIPTION model)
        {

            if (ModelState.IsValid)
            {
                model.ProductId = Convert.ToInt32(Session["ID"]);
                _db.PRODUCTDESCRIPTIONs.Add(model);
                _db.SaveChanges();
                return RedirectToAction("AddPicture", "AdminHome");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult AddPicture()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddPicture(PRODUCTPICTURE model)
        {
            if (ModelState.IsValid)
            {
                model.ProductId = Convert.ToInt32(Session["ID"]);
                _db.PRODUCTPICTUREs.Add(model);
                _db.SaveChanges();
                return RedirectToAction("AddPicture", "AdminHome");
            }
            return View(model);
        }





        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(Amazon.Models.Password model)
        {
            if (ModelState.IsValid)
            {
                model.OldPassword = Hashing.Hash(model.OldPassword);
                model.NewPassword = Hashing.Hash(model.NewPassword);
                model.ConfirmPassword = Hashing.Hash(model.ConfirmPassword);



                var user = _db.ADMINs.Where(a => a.Username.Equals(User.Identity.Name) && a.Password.Equals(model.OldPassword)).FirstOrDefault();
                if (user != null)
                {
                    user.Password = model.NewPassword;
                    _db.Entry(user).State = EntityState.Modified;
                    _db.SaveChanges();
                    return RedirectToAction("Index", "AdminHome");
                }
                else
                {
                    ViewBag.message = "Wrong Old Password";
                    return View();
                }
                
            }
            return View(model);
        }
    }
}

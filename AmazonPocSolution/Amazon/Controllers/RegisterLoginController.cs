using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Amazon.Controllers
{
    public class RegisterLoginController : Controller
    {
        XKARTEntities _db = new XKARTEntities();

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Register(CUSTOMER model)
        {
                if (ModelState.IsValid)
                {
                    model.Password = Hashing.Hash(model.Password);
                    model.ConfirmPassowrd = Hashing.Hash(model.ConfirmPassowrd);

                    _db.CUSTOMERs.Add(model);
                    _db.SaveChanges();
                    FormsAuthentication.SetAuthCookie(model.Name, false);
                    return RedirectToAction("Index", "Home");
                }
            return View(model);

        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(CUSTOMER model)
        {
            model.Password = Hashing.Hash(model.Password);
            int index = model.Email.IndexOf('@');
            if (index == -1)
            {
                var user = _db.ADMINs.Where(a => a.Username.Equals(model.Email) && a.Password.Equals(model.Password)).FirstOrDefault();
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(user.Username, false);
                    return RedirectToAction("Index", "AdminHome");
                }
                else
                {
                    ViewBag.message = "Wrong Credentials";
                    return View();
                }
            }
            else
            {
                var user = _db.CUSTOMERs.Where(a => a.Email.Equals(model.Email) && a.Password.Equals(model.Password)).FirstOrDefault();

                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(user.Name, false);
                    Session["CustomerID"] = user.CustomerId;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.message = "Wrong User Credentials";
                    return View();
                }
            }
        }

        [HttpGet]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

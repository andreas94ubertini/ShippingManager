using ShippingManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ShippingManager.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LogIn(Users u)
        {
            Users loggedUser = Db.GetUserByUsername(u.Username);
            if (loggedUser != null && loggedUser.Psw == u.Psw)
            {
                FormsAuthentication.SetAuthCookie(u.Username, false);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult Search()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Search(SearchInfo s)
        {
            Shipping shipping = Db.GetShippingById(s.IdShipping);
            Customer c = Db.GetCustomerById(shipping.IdCustomer);
            List<Update> ShippingUpdate = Db.GetShippingInfo(s.IdShipping, s.CustomerIdentity, s.IsAzienda);
            ShippingDetails details = new ShippingDetails();
            details.shipping = shipping;
            details.updateList = ShippingUpdate;
            if (s.IsAzienda)
            {
                if (s.CustomerIdentity == c.PIva)
                {
                    Session["Details"] = details;
                    return RedirectToAction("Details");
                }
                else
                {
                    TempData["NotFound"] = "Nessuna spedizione trovata";
                    return View();
                }
            }
            if (s.IsAzienda != true)
            {
                if (s.CustomerIdentity == c.Cf)
                {
                    Session["Details"] = details;
                    return RedirectToAction("Details");
                }
                else
                {
                    TempData["NotFound"] = "Nessuna spedizione trovata";
                    return View();
                }
            }
            else
            {
                TempData["NotFound"] = "Nessuna spedizione trovata";
                return View();
            }

        }
        public ActionResult Details()
        {
            if (HttpContext.Session["Details"] != null)
            {
                ShippingDetails details = HttpContext.Session["Details"] as ShippingDetails;
                return View(details);
            }
            else
            {
                return RedirectToAction("Index");
            }


        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}
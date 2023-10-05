using ShippingManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ShippingManager.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        // GET: Admin

        public ActionResult Index()
        {
            List<Customer> customerList = Db.GetAllCustomers();
            return View(customerList);
        }
        public ActionResult ManageShipments()
        {
            List<Shipping> shippingList = Db.GetAllShipments();
            return View(shippingList);
        }
        [HttpGet]
        public ActionResult UpdateStatus()
        {
            List<SelectListItem> listStatus = new List<SelectListItem>();
            SelectListItem item = new SelectListItem { Text = $"In Transito", Value = "In Transito" };
            SelectListItem item2 = new SelectListItem { Text = $"In Lavorazione", Value = "In Lavorazione" };
            SelectListItem item3 = new SelectListItem { Text = $"Arrivato nella sede locale", Value = "Arrivato nella sede locale" };
            SelectListItem item4 = new SelectListItem { Text = $"In Consegna", Value = "In Consegna" };
            SelectListItem item5 = new SelectListItem { Text = $"Consegnato", Value = "Consegnato" };
            listStatus.Add(item);
            listStatus.Add(item2);
            listStatus.Add(item3);
            listStatus.Add(item4);
            listStatus.Add(item5);
            ViewBag.ListStatus = listStatus;
            return View();
        }
        [HttpPost]
        public ActionResult UpdateStatus(Update u, int id)
        {
            List<SelectListItem> listStatus = new List<SelectListItem>();
            SelectListItem item = new SelectListItem { Text = $"In Transito", Value = "In Transito" };
            SelectListItem item2 = new SelectListItem { Text = $"In Lavorazione", Value = "In Lavorazione" };
            SelectListItem item3 = new SelectListItem { Text = $"Arrivato nella sede locale", Value = "Arrivato nella sede locale" };
            SelectListItem item4 = new SelectListItem { Text = $"In Consegna", Value = "In Consegna" };
            SelectListItem item5 = new SelectListItem { Text = $"Consegnato", Value = "Consegnato" };
            listStatus.Add(item);
            listStatus.Add(item2);
            listStatus.Add(item3);
            listStatus.Add(item4);
            listStatus.Add(item5);
            ViewBag.ListStatus = listStatus;
            DateTime currentDate = DateTime.Now;

            Db.NewUpdate(u.State, u.Location, u.Description, currentDate, id);
            Db.SetShippingStatus(u.State, id);
            return RedirectToAction("ManageShipments");
        }
        public ActionResult DeleteShipping(int id)
        {
            Db.DeleteShipping(id);
            return RedirectToAction("ManageShipments");
        }
        [HttpGet]
        public ActionResult AddShipping()
        {
            List<SelectListItem> listCustomerOpt = new List<SelectListItem>();
            List<Customer> allCustomers = Db.GetAllCustomers();
            foreach (Customer c in allCustomers)
            {
                SelectListItem item = new SelectListItem { Text = $"{c.Name} {c.Surname}", Value = $"{c.IdCustomer}" };
                listCustomerOpt.Add(item);
            }
            ViewBag.ListCustomerOpt = listCustomerOpt;
            return View();
        }
        [HttpPost]
        public ActionResult AddShipping(Shipping s)
        {
            List<SelectListItem> listCustomerOpt = new List<SelectListItem>();
            List<Customer> allCustomers = Db.GetAllCustomers();
            foreach (Customer c in allCustomers)
            {
                SelectListItem item = new SelectListItem { Text = $"{c.Name} {c.Surname}", Value = $"{c.IdCustomer}" };
                listCustomerOpt.Add(item);
            }
            ViewBag.ListCustomerOpt = listCustomerOpt;
            Db.NewShipping(s.ShippingDate, s.Weight, s.Destination, s.Address, s.Addressee, s.Price, s.ExDelivery, s.IdCustomer);

            return RedirectToAction("ManageShipments");
        }
        [HttpGet]
        public ActionResult AddCustomer()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCustomer(Customer c)
        {
            if (c.IsAzienda)
            {
                c.Cf = " ";
            }
            else
            {
                c.PIva = " ";
            }
            Db.NewCustomer(c.Name, c.Surname, c.IsAzienda, c.Cf, c.PIva);
            return RedirectToAction("Index");
        }
        public ActionResult Query()
        {
            return View();
        }
        public ActionResult DeleteUpdate(int id)
        {
            Db.DeleteUpdate(id);
            return RedirectToAction("ManageShipments");
        }
        public ActionResult DeleteCustomer(int id)
        {
            Db.DeleteCustomer(id);
            return RedirectToAction("Index");
        }
        public JsonResult GetListOrderedByDest()
        {
            List<ShippingOrderedByDest> s = Db.GetAllShippingOrderedByDest();
            return Json(s, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetOnDelivery()
        {
            List<Shipping> s = Db.GetTotalOnDelivery();
            return Json(s, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetNotDelivery()
        {
            List<Shipping> c = Db.GetNotDelivered();
            return Json(c, JsonRequestBehavior.AllowGet);
        }
    }
}
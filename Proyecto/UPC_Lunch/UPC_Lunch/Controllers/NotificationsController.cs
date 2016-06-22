using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UPC_Lunch.Models;

namespace UPC_Lunch.Controllers
{
    public class NotificationsController : Controller
    {
        private UPCLunchContext db = new UPCLunchContext();

        // GET: Notifications
        public ActionResult Index()
        {
            var notifications = db.Notifications.Where(n => n.Email == User.Identity.Name).OrderByDescending(n => n.Date).Include(n => n.Restaurante);
            return View(notifications.ToList());
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

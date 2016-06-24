using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UPC_Lunch.Helpers;
using UPC_Lunch.Models;

namespace UPC_Lunch.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ApplicationUsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UPCLunchContext dbUPC = new UPCLunchContext();

        // GET: Users
        public ActionResult Index()
        {
            var usuariosEnRoles = (from obj in db.Roles
                                   where obj.Name.Equals("User")
                                   from user in obj.Users
                                   select user.UserId);

            return View(db.Users.Where(m => usuariosEnRoles.Contains(m.Id)).ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Email,EmailConfirmed,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(applicationUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(applicationUser);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EmailConfirmed,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(applicationUser).State = EntityState.Modified;
                var user = db.Users.Find(applicationUser.Id);
                user.EmailConfirmed = applicationUser.EmailConfirmed;
                user.PhoneNumber = applicationUser.PhoneNumber;
                user.PhoneNumberConfirmed = applicationUser.PhoneNumberConfirmed;
                user.TwoFactorEnabled = applicationUser.TwoFactorEnabled;
                user.LockoutEndDateUtc = applicationUser.LockoutEndDateUtc;
                user.LockoutEnabled = applicationUser.LockoutEnabled;
                user.AccessFailedCount = applicationUser.AccessFailedCount;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(applicationUser);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser != null)
            {
                UserHelper.RemoveUser(applicationUser.Email);

                var notificaciones = dbUPC.Notifications.Where(m => m.Email == applicationUser.Email);
                dbUPC.Notifications.RemoveRange(notificaciones);
                db.SaveChanges();

                var favoritos = dbUPC.RestaurantesFavoritos.Where(m => m.Email == applicationUser.Email);
                dbUPC.RestaurantesFavoritos.RemoveRange(favoritos);
                db.SaveChanges();

                db.Users.Remove(applicationUser);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
                dbUPC.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

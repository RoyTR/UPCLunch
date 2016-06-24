using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UPC_Lunch.Helpers;
using UPC_Lunch.Models;

namespace UPC_Lunch.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class RestaurantesController : Controller
    {
        private UPCLunchContext db = new UPCLunchContext();

        // GET: Restaurantes
        public ActionResult Index()
        {
            return View(db.Restaurantes.ToList());
        }

        // GET: Restaurantes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurante restaurante = db.Restaurantes.Find(id);
            if (restaurante == null)
            {
                return HttpNotFound();
            }
            return View(restaurante);
        }

        // GET: Restaurantes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Restaurantes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Email,RestauranteId,RazonSocial,RUC,Direccion,Latitud,Longitud,AforoCompleto,Estado,MesaDisponible")] Restaurante restaurante)
        {
            if (ModelState.IsValid)
            {
                ApplicationDbContext context = new ApplicationDbContext();
                var userStore = new UserStore<ApplicationUser>(context);
                var UserManager = new UserManager<ApplicationUser>(userStore);

                var user = new ApplicationUser { UserName = restaurante.Email, Email = restaurante.Email };
                var result = await UserManager.CreateAsync(user, "password");
                if (result.Succeeded)
                {
                    UserManager.AddToRole(user.Id, "Restaurant");
                    db.Restaurantes.Add(restaurante);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                AddErrors(result);
            }

            return View(restaurante);
        }

        // GET: Restaurantes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurante restaurante = db.Restaurantes.Find(id);
            if (restaurante == null)
            {
                return HttpNotFound();
            }
            return View(restaurante);
        }

        // POST: Restaurantes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Email,RestauranteId,RazonSocial,RUC,Direccion,Latitud,Longitud,AforoCompleto,Estado,MesaDisponible")] Restaurante restaurante)
        {
            if (ModelState.IsValid)
            {
                db.Entry(restaurante).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(restaurante);
        }

        // GET: Restaurantes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurante restaurante = db.Restaurantes.Find(id);
            if (restaurante == null)
            {
                return HttpNotFound();
            }
            return View(restaurante);
        }

        // POST: Restaurantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Restaurante restaurante = db.Restaurantes.Find(id);
            if (restaurante != null)
            {
                UserHelper.RemoveUser(restaurante.Email);

                var platos = db.Platos.Where(m => m.Restaurante.Email == restaurante.Email);
                db.Platos.RemoveRange(platos);
                db.SaveChanges();

                var notificaciones = db.Notifications.Where(m => m.RestauranteId == restaurante.RestauranteId);
                db.Notifications.RemoveRange(notificaciones);
                db.SaveChanges();

                db.Restaurantes.Remove(restaurante);
                db.SaveChanges();
            }
            /*
            using (var transaction = new ApplicationDbContext().Database.BeginTransaction())
            {
                ApplicationDbContext context = new ApplicationDbContext();
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                var user = userManager.FindById(restaurante.Email);
                var rolesForUser = userManager.GetRoles(restaurante.Email);
                var logins = user.Logins;

                foreach (var login in logins.ToList())
                {
                    userManager.RemoveLogin(login.UserId, new UserLoginInfo(login.LoginProvider, login.ProviderKey));
                }
                if (rolesForUser.Count() > 0)
                {
                    foreach (var item in rolesForUser.ToList())
                    {
                        var result = userManager.RemoveFromRole(restaurante.Email, item);
                    }
                }
                userManager.Delete(user);
                transaction.Commit();
            }
            */
            return RedirectToAction("Index");
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        /*
        public void RemoveUser(string UserMail)
        {
            using (var transaction = new ApplicationDbContext().Database.BeginTransaction())
            {
                ApplicationDbContext context = new ApplicationDbContext();
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                var user = userManager.FindByEmail(UserMail);
                var rolesForUser = userManager.GetRoles(user.Id);
                var logins = user.Logins;

                foreach (var login in logins.ToList())
                {
                    userManager.RemoveLogin(login.UserId, new UserLoginInfo(login.LoginProvider, login.ProviderKey));
                }
                if (rolesForUser.Count() > 0)
                {
                    foreach (var item in rolesForUser.ToList())
                    {
                        var result = userManager.RemoveFromRole(user.Id, item);
                    }
                }
                userManager.Delete(user);
                transaction.Commit();
            }
        }
        */
    }
}

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
    public class RestauranteFavoritoesController : Controller
    {
        private UPCLunchContext db = new UPCLunchContext();

        // GET: RestauranteFavoritoes
        public ActionResult Index()
        {
            var restaurantesFavoritos = db.RestaurantesFavoritos.Include(r => r.Restaurante);
            ViewBag.Restaurantes = db.Restaurantes.ToList();
            return View(restaurantesFavoritos.ToList());
        }

        // GET: RestauranteFavoritoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RestauranteFavorito restauranteFavorito = db.RestaurantesFavoritos.Find(id);
            if (restauranteFavorito == null)
            {
                return HttpNotFound();
            }
            return View(restauranteFavorito);
        }

        // GET: RestauranteFavoritoes/Create
        public ActionResult Create()
        {
            RestauranteFavorito obj = new RestauranteFavorito();
            obj.Email = User.Identity.Name;
            ViewBag.RestauranteId = new SelectList(db.Restaurantes, "RestauranteId", "Email");
            return View(obj);
        }

        // POST: RestauranteFavoritoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RestauranteFavoritoId,Email,RestauranteId")] RestauranteFavorito restauranteFavorito)
        {
            if (ModelState.IsValid)
            {
                db.RestaurantesFavoritos.Add(restauranteFavorito);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RestauranteId = new SelectList(db.Restaurantes, "RestauranteId", "Email", restauranteFavorito.RestauranteId);
            return View(restauranteFavorito);
        }

        // GET: RestauranteFavoritoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RestauranteFavorito restauranteFavorito = db.RestaurantesFavoritos.Find(id);
            if (restauranteFavorito == null)
            {
                return HttpNotFound();
            }
            ViewBag.RestauranteId = new SelectList(db.Restaurantes, "RestauranteId", "Email", restauranteFavorito.RestauranteId);
            return View(restauranteFavorito);
        }

        // POST: RestauranteFavoritoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RestauranteFavoritoId,Email,RestauranteId")] RestauranteFavorito restauranteFavorito)
        {
            if (ModelState.IsValid)
            {
                db.Entry(restauranteFavorito).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RestauranteId = new SelectList(db.Restaurantes, "RestauranteId", "Email", restauranteFavorito.RestauranteId);
            return View(restauranteFavorito);
        }

        // GET: RestauranteFavoritoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RestauranteFavorito restauranteFavorito = db.RestaurantesFavoritos.Find(id);
            if (restauranteFavorito == null)
            {
                return HttpNotFound();
            }
            return View(restauranteFavorito);
        }

        // POST: RestauranteFavoritoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RestauranteFavorito restauranteFavorito = db.RestaurantesFavoritos.Find(id);
            db.RestaurantesFavoritos.Remove(restauranteFavorito);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ViewDishes(int id, int? NotificationId)
        {
            if (NotificationId != null)
            {
                var notificacion = db.Notifications.Where(m => m.NotificationId == NotificationId.Value).SingleOrDefault();
                if (notificacion != null)
                {
                    notificacion.Seen = true;
                    db.Entry(notificacion).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            ViewBag.Restaurante = db.Restaurantes.Where(x => x.RestauranteId == id).SingleOrDefault().RazonSocial;
            return View(db.Platos.Where(x => x.Restaurante.RestauranteId == id).ToList());
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

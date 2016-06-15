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
    public class PlatosController : Controller
    {
        private UPCLunchContext db = new UPCLunchContext();

        // GET: Platos
        public ActionResult Index()
        {
            ViewBag.MesasDisponibles = db.Restaurantes.Where(x => x.Email == User.Identity.Name).SingleOrDefault().MesaDisponible;
            return View(db.Platos.Where(x => x.Restaurante.Email == User.Identity.Name).ToList());
        }

        // GET: Platos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plato plato = db.Platos.Find(id);
            if (plato == null)
            {
                return HttpNotFound();
            }
            return View(plato);
        }

        // GET: Platos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Platos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PlatoId,Nombre,Disponible")] Plato plato)
        {
            if (ModelState.IsValid)
            {
                plato.Restaurante = (from obj in db.Restaurantes where obj.Email == User.Identity.Name select obj).SingleOrDefault();
                db.Platos.Add(plato);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(plato);
        }

        // GET: Platos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plato plato = db.Platos.Find(id);
            if (plato == null)
            {
                return HttpNotFound();
            }
            return View(plato);
        }

        // POST: Platos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PlatoId,Nombre,Disponible,Restaurante")] Plato plato)
        {
            if (ModelState.IsValid)
            {
                plato.Restaurante = (from obj in db.Restaurantes where obj.Email == User.Identity.Name select obj).SingleOrDefault();
                db.Entry(plato).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(plato);
        }

        // GET: Platos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plato plato = db.Platos.Find(id);
            if (plato == null)
            {
                return HttpNotFound();
            }
            return View(plato);
        }

        // POST: Platos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Plato plato = db.Platos.Find(id);
            db.Platos.Remove(plato);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ActualizarDisponibilidadMesa()
        {
            Restaurante rest = (from obj in db.Restaurantes where obj.Email == User.Identity.Name select obj).SingleOrDefault();
            if (rest.MesaDisponible == true)
                rest.MesaDisponible = false;
            else
                rest.MesaDisponible = true;
            db.Entry(rest).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
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

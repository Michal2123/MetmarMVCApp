using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MetmarMVCApp.Models;

namespace MetmarMVCApp.Controllers
{
    public class FakturasController : Controller
    {
        private MetmarModel db = new MetmarModel();

        // GET: Fakturas
        public ActionResult Index()
        {
            var faktura = db.Faktura.Include(f => f.Klienci);
            return View(faktura.ToList());
        }

        // GET: Fakturas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faktura faktura = db.Faktura.Find(id);
            if (faktura == null)
            {
                return HttpNotFound();
            }
            return View(faktura);
        }

        // GET: Fakturas/Create
        public ActionResult Create()
        {
            ViewBag.IdKlienta = new SelectList(db.Klienci, "Id", "Imie");
            return View();
        }

        // POST: Fakturas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdKlienta,Data,Suma")] Faktura faktura)
        {
            if (ModelState.IsValid)
            {
                db.Faktura.Add(faktura);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdKlienta = new SelectList(db.Klienci, "Id", "Imie", faktura.IdKlienta);
            return View(faktura);
        }

        // GET: Fakturas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faktura faktura = db.Faktura.Find(id);
            if (faktura == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdKlienta = new SelectList(db.Klienci, "Id", "Imie", faktura.IdKlienta);
            return View(faktura);
        }

        // POST: Fakturas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdKlienta,Data,Suma")] Faktura faktura)
        {
            if (ModelState.IsValid)
            {
                db.Entry(faktura).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdKlienta = new SelectList(db.Klienci, "Id", "Imie", faktura.IdKlienta);
            return View(faktura);
        }

        // GET: Fakturas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faktura faktura = db.Faktura.Find(id);
            if (faktura == null)
            {
                return HttpNotFound();
            }
            return View(faktura);
        }

        // POST: Fakturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Faktura faktura = db.Faktura.Find(id);
            db.Faktura.Remove(faktura);
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

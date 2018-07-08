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
    public class ClientsController : Controller
    {
        private MetmarModel db = new MetmarModel();

        // GET: Clients
        public ActionResult Index(string searchLastNm)
        {
            var ClientQry = from d in db.Klienci
                            select d;

            if (!String.IsNullOrEmpty(searchLastNm))
            {
                ClientQry = ClientQry.Where(c => c.Nazwisko.Contains(searchLastNm));
            }

            return View(ClientQry.ToList());
        }

        // GET: Clients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Klienci klienci = db.Klienci.Find(id);
            if (klienci == null)
            {
                return HttpNotFound();
            }
            return View(klienci);
        }

        // GET: Clients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Imie,Nazwisko,Pesel,Telefon,Adres,IsActive")] Klienci klienci)
        {
            if (ModelState.IsValid)
            {
                db.Klienci.Add(klienci);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(klienci);
        }

        // GET: Clients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Klienci klienci = db.Klienci.Find(id);
            if (klienci == null)
            {
                return HttpNotFound();
            }
            return View(klienci);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Imie,Nazwisko,Pesel,Telefon,Adres,IsActive")] Klienci klienci)
        {
            if (ModelState.IsValid)
            {
                db.Entry(klienci).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(klienci);
        }

        // GET: Clients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Klienci klienci = db.Klienci.Find(id);
            if (klienci == null)
            {
                return HttpNotFound();
            }
            return View(klienci);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Klienci klienci = db.Klienci.Find(id);
            db.Klienci.Remove(klienci);
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

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
    public class ItemsController : Controller
    {
        private MetmarModel db = new MetmarModel();
        private static List<Order> list = new List<Order>();
        // GET: Items
        public ActionResult Index(string categoryName , string searchString)
        {

            var CategoryLst = new List<string>();
            var CategoryQry = from d in db.Kategorie
                              orderby d.Nazwa
                              select d.Nazwa;

            CategoryLst.AddRange(CategoryQry.Distinct());
            ViewBag.categoryName = new SelectList(CategoryLst);


            var przedmioty = db.Przedmioty.Include(p => p.Kategorie);

            if (!String.IsNullOrEmpty(searchString))
            {
                przedmioty = przedmioty.Where(s => s.Nazwa.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(categoryName))
            {
                przedmioty = przedmioty.Where(x => x.Kategorie.Nazwa.Contains(categoryName));
            }

            return View(przedmioty.ToList());
        }

        // GET: Items/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Przedmioty przedmioty = db.Przedmioty.Find(id);
            if (przedmioty == null)
            {
                return HttpNotFound();
            }
            return View(przedmioty);
        }

        // GET: Items/Create
        public ActionResult Create()
        {
            ViewBag.IdKategorii = new SelectList(db.Kategorie, "Id", "Nazwa");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nazwa,IdKategorii,Kaucja,StawkaDzien,StawkaGodzinowa,Cena,IsPrice,Wartosc,IsSki")] Przedmioty przedmioty)
        {
            if (ModelState.IsValid)
            {
                db.Przedmioty.Add(przedmioty);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdKategorii = new SelectList(db.Kategorie, "Id", "Nazwa", przedmioty.IdKategorii);
            return View(przedmioty);
        }

        // GET: Items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Przedmioty przedmioty = db.Przedmioty.Find(id);
            if (przedmioty == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdKategorii = new SelectList(db.Kategorie, "Id", "Nazwa", przedmioty.IdKategorii);
            return View(przedmioty);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nazwa,IdKategorii,Kaucja,StawkaDzien,StawkaGodzinowa,Cena,IsPrice,Wartosc,IsSki")] Przedmioty przedmioty)
        {
            if (ModelState.IsValid)
            {
                db.Entry(przedmioty).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdKategorii = new SelectList(db.Kategorie, "Id", "Nazwa", przedmioty.IdKategorii);
            return View(przedmioty);
        }

        // GET: Items/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Przedmioty przedmioty = db.Przedmioty.Find(id);
            if (przedmioty == null)
            {
                return HttpNotFound();
            }
            return View(przedmioty);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Przedmioty przedmioty = db.Przedmioty.Find(id);
            db.Przedmioty.Remove(przedmioty);
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

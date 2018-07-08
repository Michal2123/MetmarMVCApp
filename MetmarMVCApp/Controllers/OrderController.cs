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
    public class OrderController : Controller
    {
        private MetmarModel db = new MetmarModel();
        private static List<Order> orderList = new List<Order>();

        //public OrderController(List<Order> List)
        //{
        //    orderList = List;
        //}

        public OrderController()
        {

        }

        // GET: Order
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost, ActionName("Index")]
        public ActionResult IndexConfirm()
        {
            return RedirectToAction("ClientTable");
        }

        public ActionResult OrderTable()
        {
            return View(orderList);
        }

        public ActionResult ClientTable(string searchLastNm)
        {

            var ClientQry = from d in db.Klienci
                            select d;

            if (!String.IsNullOrEmpty(searchLastNm))
            {
                ClientQry = ClientQry.Where(c => c.Nazwisko.Contains(searchLastNm));
            }

            return View(ClientQry.ToList());
        }

        [HttpPost, ActionName("ClientTable")]
        public ActionResult ClientTableNext()
        {
            return RedirectToAction("ItemTable");
        }

        public ActionResult ItemTable(string categoryName, string searchString)
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

        [HttpPost, ActionName("ItemTable")]
        public ActionResult ItemTableNext()
        {
            return RedirectToAction("OrderTable");
        }

        // Get: Order/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order orderItem = orderList.Find(i => i.Item.Id == id);
            if (orderItem == null)
            {
                return HttpNotFound();
            }
            var Item = orderItem.Item as Przedmioty;
            return View(Item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nazwa,IdKategorii,Kaucja,StawkaDzien,StawkaGodzinowa,Cena,IsPrice,Wartosc,IsSki")] Przedmioty newItem)
        {
            if (ModelState.IsValid)
            {
                Order oldOrder = orderList.Find(i => i.Item.Id == newItem.Id);
                orderList.Remove(oldOrder);
                var newOrder = new Order
                {
                    Item = newItem,
                    Count = oldOrder.Count,
                    DateCreated = oldOrder.DateCreated
                };
                orderList.Add(newOrder);
                return RedirectToAction("OrderTable");
            }
            return View(newItem);
        }

        // Get: Order/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order orderItem = orderList.Find(i => i.Item.Id == id);
            if (orderItem == null)
            {
                return HttpNotFound();
            }
            return View(orderItem);
        }

        // Post: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            Order orderItem = orderList.Find(i => i.Item.Id == id);
            if (orderItem.Count > 1)
            {
                orderList.Remove(orderItem);
                orderItem.Count--;
                orderList.Add(orderItem);
            }
            else
                orderList.Remove(orderItem);

            return RedirectToAction("OrderTable");
        }

        public ActionResult AddToOrderDetails(int? id)
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

        [HttpPost, ActionName("AddToOrderDetails")]
        [ValidateAntiForgeryToken]
        public ActionResult AddToOrderConfirm(int id, bool isDay = false)
        {
            var item = orderList.FirstOrDefault(i => i.Item.Id == id);
            Przedmioty przedmioty = db.Przedmioty.Find(id);
            if (item != null)
            {
                item.Count++;
            }
            else
            {
                var order = new Order
                {
                    Item = przedmioty,
                    Count = 1,
                    DateCreated = DateTime.Now
                };
                if (isDay == true)
                    order.ToPay = przedmioty.StawkaDzien;
                else
                {
                    order.ToPay = przedmioty.StawkaGodzinowa;
                    order.ToPay += przedmioty.Cena;
                }
                orderList.Add(order);
            }

            return RedirectToAction("ItemTable");
        }

        public ActionResult Select(int? id)
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

        [HttpPost, ActionName("Select")]
        [ValidateAntiForgeryToken]
        public ActionResult SelectConfirm(int id)
        {
            Klienci klienci = db.Klienci.Find(id);
            Session["Client"] = klienci;
            return RedirectToAction("ClientTable");
        }
    }
}
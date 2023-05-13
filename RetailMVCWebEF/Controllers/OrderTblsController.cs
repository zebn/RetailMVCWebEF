using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RetailMVCWebEF.Models.ML;

namespace RetailMVCWebEF
{
    public class OrderTblsController : Controller
    {
        private GestionHosteleriaGenNHibernateEntities1 db = new GestionHosteleriaGenNHibernateEntities1();

        // GET: OrderTbls
        public ActionResult Index()
        {
            var orderTbls = db.OrderTbls.Include(o => o.TableTbl).Include(o => o.Restaurant);
            return View(orderTbls.ToList());
        }

        // GET: OrderTbls/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderTbl orderTbl = db.OrderTbls.Find(id);
            if (orderTbl == null)
            {
                return HttpNotFound();
            }
            return View(orderTbl);
        }

        // GET: OrderTbls/Create
        public ActionResult Create()
        {
            ViewBag.FK_id_idTable = new SelectList(db.TableTbls, "id", "id");
            ViewBag.FK_id_idRestaurant = new SelectList(db.Restaurants, "id", "name");
            return View();
        }

        // POST: OrderTbls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,creationTime,isActive,total,isPaid,FK_id_idTable,FK_id_idRestaurant")] OrderTbl orderTbl)
        {
            if (ModelState.IsValid)
            {
                db.OrderTbls.Add(orderTbl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FK_id_idTable = new SelectList(db.TableTbls, "id", "id", orderTbl.FK_id_idTable);
            ViewBag.FK_id_idRestaurant = new SelectList(db.Restaurants, "id", "name", orderTbl.FK_id_idRestaurant);
            return View(orderTbl);
        }

        // GET: OrderTbls/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderTbl orderTbl = db.OrderTbls.Find(id);
            if (orderTbl == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_id_idTable = new SelectList(db.TableTbls, "id", "id", orderTbl.FK_id_idTable);
            ViewBag.FK_id_idRestaurant = new SelectList(db.Restaurants, "id", "name", orderTbl.FK_id_idRestaurant);
            return View(orderTbl);
        }

        // POST: OrderTbls/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,creationTime,isActive,total,isPaid,FK_id_idTable,FK_id_idRestaurant")] OrderTbl orderTbl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderTbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_id_idTable = new SelectList(db.TableTbls, "id", "id", orderTbl.FK_id_idTable);
            ViewBag.FK_id_idRestaurant = new SelectList(db.Restaurants, "id", "name", orderTbl.FK_id_idRestaurant);
            return View(orderTbl);
        }

        // GET: OrderTbls/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderTbl orderTbl = db.OrderTbls.Find(id);
            if (orderTbl == null)
            {
                return HttpNotFound();
            }
            return View(orderTbl);
        }

        // POST: OrderTbls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderTbl orderTbl = db.OrderTbls.Find(id);
            db.OrderTbls.Remove(orderTbl);
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

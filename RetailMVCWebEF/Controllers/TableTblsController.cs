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
    public class TableTblsController : Controller
    {
        private GestionHosteleriaGenNHibernateEntities1 db = new GestionHosteleriaGenNHibernateEntities1();

        // GET: TableTbls
        public ActionResult Index()
        {
            var tableTbls = db.TableTbls.Include(t => t.Restaurant);
            return View(tableTbls.ToList());
        }

        // GET: TableTbls/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TableTbl tableTbl = db.TableTbls.Find(id);
            if (tableTbl == null)
            {
                return HttpNotFound();
            }
            return View(tableTbl);
        }

        // GET: TableTbls/Create
        public ActionResult Create()
        {
            ViewBag.FK_id_idRestaurant = new SelectList(db.Restaurants, "id", "name");
            return View();
        }

        // POST: TableTbls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,isAvailable,code,capacity,FK_id_idRestaurant")] TableTbl tableTbl)
        {
            if (ModelState.IsValid)
            {
                db.TableTbls.Add(tableTbl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FK_id_idRestaurant = new SelectList(db.Restaurants, "id", "name", tableTbl.FK_id_idRestaurant);
            return View(tableTbl);
        }

        // GET: TableTbls/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TableTbl tableTbl = db.TableTbls.Find(id);
            if (tableTbl == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_id_idRestaurant = new SelectList(db.Restaurants, "id", "name", tableTbl.FK_id_idRestaurant);
            return View(tableTbl);
        }

        // POST: TableTbls/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,isAvailable,code,capacity,FK_id_idRestaurant")] TableTbl tableTbl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tableTbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_id_idRestaurant = new SelectList(db.Restaurants, "id", "name", tableTbl.FK_id_idRestaurant);
            return View(tableTbl);
        }

        // GET: TableTbls/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TableTbl tableTbl = db.TableTbls.Find(id);
            if (tableTbl == null)
            {
                return HttpNotFound();
            }
            return View(tableTbl);
        }

        // POST: TableTbls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TableTbl tableTbl = db.TableTbls.Find(id);
            db.TableTbls.Remove(tableTbl);
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

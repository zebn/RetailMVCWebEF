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
    public class BooksController : Controller
    {
        private GestionHosteleriaGenNHibernateEntities1 db = new GestionHosteleriaGenNHibernateEntities1();

        // GET: Books
        public ActionResult Index()
        {
            var books = db.Books.Include(b => b.Restaurant).Include(b => b.TableTbl);
            return View(books.ToList());
        }

        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            ViewBag.FK_id_idRestaurant = new SelectList(db.Restaurants, "id", "name");
            ViewBag.FK_id_idTableTbl = new SelectList(db.TableTbls, "id", "id");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idBook,clientNumber,name,phone,time,FK_id_idTableTbl,FK_id_idRestaurant")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FK_id_idRestaurant = new SelectList(db.Restaurants, "id", "name", book.FK_id_idRestaurant);
            ViewBag.FK_id_idTableTbl = new SelectList(db.TableTbls, "id", "id", book.FK_id_idTableTbl);
            return View(book);
        }

        // GET: Books/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_id_idRestaurant = new SelectList(db.Restaurants, "id", "name", book.FK_id_idRestaurant);
            ViewBag.FK_id_idTableTbl = new SelectList(db.TableTbls, "id", "id", book.FK_id_idTableTbl);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idBook,clientNumber,name,phone,time,FK_id_idTableTbl,FK_id_idRestaurant")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_id_idRestaurant = new SelectList(db.Restaurants, "id", "name", book.FK_id_idRestaurant);
            ViewBag.FK_id_idTableTbl = new SelectList(db.TableTbls, "id", "id", book.FK_id_idTableTbl);
            return View(book);
        }

        // GET: Books/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
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

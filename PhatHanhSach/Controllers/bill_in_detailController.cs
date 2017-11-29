using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PhatHanhSach.Models;

namespace PhatHanhSach.Controllers
{
    public class bill_in_detailController : Controller
    {
        private PHSEntities db = new PHSEntities();

        // GET: bill_in_detail
        public ActionResult Index()
        {
            var bill_in_detail = db.bill_in_detail.Include(b => b.bill_in).Include(b => b.book);
            return View(bill_in_detail.ToList());
        }

        // GET: bill_in_detail/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bill_in_detail bill_in_detail = db.bill_in_detail.Find(id);
            if (bill_in_detail == null)
            {
                return HttpNotFound();
            }
            return View(bill_in_detail);
        }

        // GET: bill_in_detail/Create
        public ActionResult Create()
        {
            ViewBag.id_bill_in = new SelectList(db.bill_in, "id", "rc");
            ViewBag.id_book = new SelectList(db.books, "id", "name");
            return View();
        }

        // POST: bill_in_detail/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "id,id_bill_in,id_book,quantity")] bill_in_detail bill_in_detail)
        {
            if (ModelState.IsValid)
            {
                db.bill_in_detail.Add(bill_in_detail);
                db.SaveChanges();
                //UpdateBookStorage
                var book = db.books.Find(bill_in_detail.id_book);
                int num = bill_in_detail.quantity ?? 0;
                book.stock = book.stock + num;
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { con = "success" });
            }

            ViewBag.id_bill_in = new SelectList(db.bill_in, "id", "rc", bill_in_detail.id_bill_in);
            ViewBag.id_book = new SelectList(db.books, "id", "name", bill_in_detail.id_book);
            return Json(new { con="error" });

        }

        // GET: bill_in_detail/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bill_in_detail bill_in_detail = db.bill_in_detail.Find(id);
            if (bill_in_detail == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_bill_in = new SelectList(db.bill_in, "id", "rc", bill_in_detail.id_bill_in);
            ViewBag.id_book = new SelectList(db.books, "id", "name", bill_in_detail.id_book);
            return View(bill_in_detail);
        }

        // POST: bill_in_detail/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_bill_in,id_book,quantity")] bill_in_detail bill_in_detail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bill_in_detail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_bill_in = new SelectList(db.bill_in, "id", "rc", bill_in_detail.id_bill_in);
            ViewBag.id_book = new SelectList(db.books, "id", "name", bill_in_detail.id_book);
            return View(bill_in_detail);
        }

        // GET: bill_in_detail/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bill_in_detail bill_in_detail = db.bill_in_detail.Find(id);
            if (bill_in_detail == null)
            {
                return HttpNotFound();
            }
            return View(bill_in_detail);
        }

        // POST: bill_in_detail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            bill_in_detail bill_in_detail = db.bill_in_detail.Find(id);
            db.bill_in_detail.Remove(bill_in_detail);
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

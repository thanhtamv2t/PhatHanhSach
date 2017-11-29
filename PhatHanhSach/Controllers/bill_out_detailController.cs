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
    public class bill_out_detailController : Controller
    {
        private PHSEntities db = new PHSEntities();

        // GET: bill_out_detail
        public ActionResult Index()
        {
            var bill_out_detail = db.bill_out_detail.Include(b => b.bill_out).Include(b => b.book);
            return View(bill_out_detail.ToList());
        }

        // GET: bill_out_detail/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bill_out_detail bill_out_detail = db.bill_out_detail.Find(id);
            if (bill_out_detail == null)
            {
                return HttpNotFound();
            }
            return View(bill_out_detail);
        }

        // GET: bill_out_detail/Create
        public ActionResult Create()
        {
            ViewBag.id_bill_out = new SelectList(db.bill_out, "id", "rc");
            ViewBag.id_book = new SelectList(db.books, "id", "name");
            return View();
        }

        // POST: bill_out_detail/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "id,id_bill_out,id_book,quantity")] bill_out_detail bill_out_detail)
        {
            if (ModelState.IsValid)
            {
                db.bill_out_detail.Add(bill_out_detail);
                db.SaveChanges();
                //Update Book Storage
                var book_id = bill_out_detail.id_book;
                var book = db.books.Find(book_id);
                book.stock = book.stock - bill_out_detail.quantity;
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { con="added" });
            }

            return Json(new { con = "error" });
        }

        // GET: bill_out_detail/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bill_out_detail bill_out_detail = db.bill_out_detail.Find(id);
            if (bill_out_detail == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_bill_out = new SelectList(db.bill_out, "id", "rc", bill_out_detail.id_bill_out);
            ViewBag.id_book = new SelectList(db.books, "id", "name", bill_out_detail.id_book);
            return View(bill_out_detail);
        }

        // POST: bill_out_detail/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_bill_out,id_book,quantity")] bill_out_detail bill_out_detail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bill_out_detail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_bill_out = new SelectList(db.bill_out, "id", "rc", bill_out_detail.id_bill_out);
            ViewBag.id_book = new SelectList(db.books, "id", "name", bill_out_detail.id_book);
            return View(bill_out_detail);
        }

        // GET: bill_out_detail/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bill_out_detail bill_out_detail = db.bill_out_detail.Find(id);
            if (bill_out_detail == null)
            {
                return HttpNotFound();
            }
            return View(bill_out_detail);
        }

        // POST: bill_out_detail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            bill_out_detail bill_out_detail = db.bill_out_detail.Find(id);
            db.bill_out_detail.Remove(bill_out_detail);
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

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
    public class bill_inController : Controller
    {
        private PHSEntities db = new PHSEntities();

        // GET: bill_in
        public ActionResult Index()
        {
            var bill_in = db.bill_in.Include(b => b.publisher);
            return View(bill_in.ToList());
        }

        // GET: bill_in/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bill_in bill_in = db.bill_in.Find(id);
            if (bill_in == null)
            {
                return HttpNotFound();
            }
            ViewBag.detail = db.bill_in_detail.Where(q => q.id_bill_in == id).Include(b => b.book);
            return View(bill_in);
        }

        // GET: bill_in/Create
        public ActionResult Create()
        {
            ViewBag.id_publisher = new SelectList(db.publishers, "id", "name");
            ViewBag.id_book = new SelectList(db.books, "id", "name");
            return View();
        }
        public ActionResult paid_update(int id)
        {
            var m = db.bill_in.Find(id);
            m.status = 1;
            db.Entry(m).State = EntityState.Modified;
            db.SaveChanges();
            return Json(new {  });

        }
        // POST: bill_in/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,id_publisher,total,date,rc")] bill_in bill_in)
        {
            if (ModelState.IsValid)
            {
                bill_in.status = 0;
                db.bill_in.Add(bill_in);
                db.SaveChanges();
                int lastid = db.bill_in.Max(item => item.id);
                return Json(new { con="success", inserted =lastid });
            }

            ViewBag.id_publisher = new SelectList(db.publishers, "id", "name", bill_in.id_publisher);
            return Json(new { con = "error" });
        }

        // GET: bill_in/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bill_in bill_in = db.bill_in.Find(id);
            if (bill_in == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_publisher = new SelectList(db.publishers, "id", "name", bill_in.id_publisher);
            return View(bill_in);
        }

        // POST: bill_in/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_publisher,total,date,rc")] bill_in bill_in)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bill_in).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_publisher = new SelectList(db.publishers, "id", "name", bill_in.id_publisher);
            return View(bill_in);
        }

        // GET: bill_in/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bill_in bill_in = db.bill_in.Find(id);
            if (bill_in == null)
            {
                return HttpNotFound();
            }
            return View(bill_in);
        }

        // POST: bill_in/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            bill_in bill_in = db.bill_in.Find(id);
            db.bill_in.Remove(bill_in);
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

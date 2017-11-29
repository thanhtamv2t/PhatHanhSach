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
    public class bill_outController : Controller
    {
        private PHSEntities db = new PHSEntities();

        // GET: bill_out
        public ActionResult Index()
        {
            var bill_out = db.bill_out.Include(b => b.agent);
            return View(bill_out.ToList());
        }

        // GET: bill_out/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bill_out bill_out = db.bill_out.Find(id);
            ViewBag.list = db.bill_out_detail.Where(q=>q.id_bill_out == id).Include(b => b.book);
            if (bill_out == null)
            {
                return HttpNotFound();
            }
            return View(bill_out);
        }

        // GET: bill_out/Create
        public ActionResult Create()
        {
            ViewBag.id_agent = new SelectList(db.agents, "id", "name");
            ViewBag.id_book = new SelectList(db.books, "id", "name");
            return View();
        }

        // POST: bill_out/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,id_agent,total,date,rc")] bill_out bill_out)
        {
            if (ModelState.IsValid)
            {
                db.bill_out.Add(bill_out);
                db.SaveChanges();
                int lastid = db.bill_out.Max(item => item.id);
                return Json(new { inserted = lastid });
            }

            ViewBag.id_agent = new SelectList(db.agents, "id", "name", bill_out.id_agent);
            return Json(new { type="error",message="Xảy ra lỗi!" });
        }
        [HttpPost]
        public ActionResult getowe(int? agent_id)
        {
            double? payment = db.agent_payment.Where(a => a.id_agent == agent_id).AsEnumerable().Sum(x => x.amount);
            double? totalBill = db.bill_out.Where(a => a.id_agent == agent_id).AsEnumerable().Sum(b => b.total);
            double? owe = totalBill - payment;
            
            return Json(new { totalBill = totalBill,payment=payment,owe=owe });
        }
        // GET: bill_out/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bill_out bill_out = db.bill_out.Find(id);
            if (bill_out == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_agent = new SelectList(db.agents, "id", "name", bill_out.id_agent);
            return View(bill_out);
        }

        // POST: bill_out/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_agent,total,date,rc")] bill_out bill_out)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bill_out).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_agent = new SelectList(db.agents, "id", "name", bill_out.id_agent);
            return View(bill_out);
        }

        // GET: bill_out/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bill_out bill_out = db.bill_out.Find(id);
            if (bill_out == null)
            {
                return HttpNotFound();
            }
            return View(bill_out);
        }

        public ActionResult getB(int? agent_id,DateTime? date)
        {
            var billout = db.bill_out_detail.Where(q => q.bill_out.id_agent == agent_id && q.bill_out.date <= date).Select(a => new
            {
                bookname = a.book.name,
                bookid = a.id_book,
                qt = a.quantity,
            });
            return Json(billout,JsonRequestBehavior.AllowGet);
            
        }

        // POST: bill_out/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            bill_out bill_out = db.bill_out.Find(id);
            //delete bill_out_detail first
            db.bill_out_detail.RemoveRange(db.bill_out_detail.Where(c=>c.id_bill_out==id));
            //Delete bill_out
            db.bill_out.Remove(bill_out);
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

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
    public class agent_paymentController : Controller
    {
        private PHSEntities db = new PHSEntities();

        // GET: agent_payment
        public ActionResult Index()
        {
            var agent_payment = db.agent_payment.Include(a => a.agent);
            return View(agent_payment.ToList());
        }

        // GET: agent_payment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            agent_payment agent_payment = db.agent_payment.Find(id);
            if (agent_payment == null)
            {
                return HttpNotFound();
            }
            ViewBag.list = db.agent_payment_detail.Where(w => w.id_payment == id).ToList();
            return View(agent_payment);
        }

        // GET: agent_payment/Create
        public ActionResult Create()
        {
            ViewBag.id_agent = new SelectList(db.agents, "id", "name");
            ViewBag.id_book = new SelectList(db.books, "id", "name");
            return View();
        }

        // POST: agent_payment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "id,id_agent,amount,date")] agent_payment agent_payment)
        {
            if (ModelState.IsValid)
            {
                db.agent_payment.Add(agent_payment);
                db.SaveChanges();
                int lastid = db.agent_payment.Max(item => item.id);
                return Json(new { con="success",inserted=lastid });
            }

            ViewBag.id_agent = new SelectList(db.agents, "id", "name", agent_payment.id_agent);
            return Json(new { con="error" });
        }

        // GET: agent_payment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            agent_payment agent_payment = db.agent_payment.Find(id);
            if (agent_payment == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_agent = new SelectList(db.agents, "id", "name", agent_payment.id_agent);
            return View(agent_payment);
        }

        // POST: agent_payment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_agent,amount,date")] agent_payment agent_payment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(agent_payment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_agent = new SelectList(db.agents, "id", "name", agent_payment.id_agent);
            return View(agent_payment);
        }

        // GET: agent_payment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            agent_payment agent_payment = db.agent_payment.Find(id);
            if (agent_payment == null)
            {
                return HttpNotFound();
            }
            return View(agent_payment);
        }
        public ActionResult getP(int? agent_id, DateTime? date)
        {
            var payment = db.agent_payment_detail.Where(q => q.agent_payment.date <= date && q.agent_payment.id_agent == agent_id).Select(a => new
            {
                bookname = a.book.name,
                bookid = a.id_book,
                qt = a.quantity,
            });

            return Json(payment, JsonRequestBehavior.AllowGet);

        }
        // POST: agent_payment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            agent_payment agent_payment = db.agent_payment.Find(id);
            db.agent_payment.Remove(agent_payment);
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

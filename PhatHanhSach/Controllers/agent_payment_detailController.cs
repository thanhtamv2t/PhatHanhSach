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
    public class agent_payment_detailController : Controller
    {
        private PHSEntities db = new PHSEntities();

        // GET: agent_payment_detail
        public ActionResult Index()
        {
            var agent_payment_detail = db.agent_payment_detail.Include(a => a.agent_payment).Include(a => a.book);
            return View(agent_payment_detail.ToList());
        }

        // GET: agent_payment_detail/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            agent_payment_detail agent_payment_detail = db.agent_payment_detail.Find(id);
            if (agent_payment_detail == null)
            {
                return HttpNotFound();
            }
            return View(agent_payment_detail);
        }

        // GET: agent_payment_detail/Create
        public ActionResult Create()
        {
            ViewBag.id_payment = new SelectList(db.agent_payment, "id", "id");
            ViewBag.id_book = new SelectList(db.books, "id", "name");
            return View();
        }

        // POST: agent_payment_detail/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "id,id_payment,id_book,quantity")] agent_payment_detail agent_payment_detail)
        {
            if (ModelState.IsValid)
            {
                db.agent_payment_detail.Add(agent_payment_detail);
                db.SaveChanges();
                return Json(new { con = "success" });
            }

            ViewBag.id_payment = new SelectList(db.agent_payment, "id", "id", agent_payment_detail.id_payment);
            ViewBag.id_book = new SelectList(db.books, "id", "name", agent_payment_detail.id_book);
            return Json(new { con = "error" });
        }

        // GET: agent_payment_detail/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            agent_payment_detail agent_payment_detail = db.agent_payment_detail.Find(id);
            if (agent_payment_detail == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_payment = new SelectList(db.agent_payment, "id", "id", agent_payment_detail.id_payment);
            ViewBag.id_book = new SelectList(db.books, "id", "name", agent_payment_detail.id_book);
            return View(agent_payment_detail);
        }

        // POST: agent_payment_detail/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_payment,id_book,quantity")] agent_payment_detail agent_payment_detail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(agent_payment_detail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_payment = new SelectList(db.agent_payment, "id", "id", agent_payment_detail.id_payment);
            ViewBag.id_book = new SelectList(db.books, "id", "name", agent_payment_detail.id_book);
            return View(agent_payment_detail);
        }

        // GET: agent_payment_detail/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            agent_payment_detail agent_payment_detail = db.agent_payment_detail.Find(id);
            if (agent_payment_detail == null)
            {
                return HttpNotFound();
            }
            return View(agent_payment_detail);
        }

        // POST: agent_payment_detail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            agent_payment_detail agent_payment_detail = db.agent_payment_detail.Find(id);
            db.agent_payment_detail.Remove(agent_payment_detail);
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

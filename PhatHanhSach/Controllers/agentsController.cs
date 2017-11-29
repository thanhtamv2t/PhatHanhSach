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
    public class agentsController : Controller
    {
        private PHSEntities db = new PHSEntities();

        // GET: agents
        public ActionResult Index()
        {
            return View(db.agents.ToList());
        }

        // GET: agents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            agent agent = db.agents.Find(id);
            if (agent == null)
            {
                return HttpNotFound();
            }
            double? payment = db.agent_payment.Where(a => a.id_agent == id).AsEnumerable().Sum(x => x.amount);
            double? totalBill = db.bill_out.Where(a => a.id_agent == id).AsEnumerable().Sum(b => b.total);
            double? owe = totalBill - payment;
            if (owe == null) owe = 0;
            if (payment == null) payment = 0;
            ViewBag.owe = owe;
            ViewBag.paid = payment;
            ViewBag.aid = agent.id;
            return View(agent);
        }
        [HttpPost]
        public ActionResult getDate(int agent_id,DateTime date)
        {
            double? payment = db.agent_payment.Where(a => a.id_agent == agent_id && a.date <= date).AsEnumerable().Sum(x => x.amount);
            double? totalBill = db.bill_out.Where(a => a.id_agent == agent_id && a.date<=date).AsEnumerable().Sum(b => b.total);
            double? owe = totalBill - payment;
            if (payment == null) payment = 0;
            if (owe == null) owe = 0;
            return Json(new { owe=owe,paid=payment });
        }
        // GET: agents/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: agents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,addr,phone")] agent agent)
        {
            if (ModelState.IsValid)
            {
                db.agents.Add(agent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(agent);
        }

        // GET: agents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            agent agent = db.agents.Find(id);
            if (agent == null)
            {
                return HttpNotFound();
            }
            return View(agent);
        }

        // POST: agents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,addr,phone")] agent agent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(agent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(agent);
        }

        // GET: agents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            agent agent = db.agents.Find(id);
            if (agent == null)
            {
                return HttpNotFound();
            }
            return View(agent);
        }

        // POST: agents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            agent agent = db.agents.Find(id);
            db.agents.Remove(agent);
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

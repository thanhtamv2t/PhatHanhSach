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
    public class agent_storageController : Controller
    {
        private PHSEntities db = new PHSEntities();

        // GET: agent_storage
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.agent_id = id;
            var agent_storage = db.agent_storage.Where(a=>a.id_agent==id).Include(a => a.agent).Include(a => a.book);
            return View(agent_storage.ToList());
        }

        // GET: agent_storage/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            agent_storage agent_storage = db.agent_storage.Find(id);
            if (agent_storage == null)
            {
                return HttpNotFound();
            }
            return View(agent_storage);
        }

        // GET: agent_storage/Create
        public ActionResult Create()
        {
            ViewBag.id_agent = new SelectList(db.agents, "id", "name");
            ViewBag.id_book = new SelectList(db.books, "id", "name");
            return View();
        }

        // POST: agent_storage/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        public int checkBook(int agent_id,int book_id)
        {
            var agent = db.agent_storage.Where(a=>a.id_agent == agent_id && a.id_book == book_id);
            var num = agent.Count();
            Boolean rt = false;
            if (num > 0) return num;
            return 0;
            
        }
        [HttpPost]
        public int getStore(int agent_id,int book_id)
        {
            var num = 0;
            if (checkBook(agent_id,book_id)==0)
            {
                return 0;
            }
            else
            {
                var agent = db.agent_storage.Where(a => a.id_agent == agent_id && a.id_book == book_id);
                
                foreach (var item in agent)
                {
                    num = item.quantity;
                    break;
                }
            }
            return num;
        }
        [HttpPost]
        public ActionResult addStore(int agent_id,int id_book,int quantity)
        {
            //Check Exists
            int check = checkBook(agent_id, id_book);
            if(check >0)
            {
                updateStore(agent_id, id_book, quantity);
                return Json(new { con="update" });
            }
            agent_storage stg = new agent_storage();
            stg.id_agent = agent_id;
            stg.id_book = id_book;
            stg.quantity = quantity;
            db.agent_storage.Add(stg);
            db.SaveChanges();
            return Json(new { con = "add" });
        }
        public int updateStore(int agent_id,int book_id,int quantity)
        {
            var stg = db.agent_storage.Single(q=>q.id_agent==agent_id && q.id_book == book_id);
            stg.quantity = stg.quantity + quantity;
            db.Entry(stg).State = EntityState.Modified;
            db.SaveChanges();
            return 1;
        }
        [HttpPost]
        public ActionResult subStore(int agent_id, int id_book, int quantity)
        {
            var stg = db.agent_storage.Single(q => q.id_agent == agent_id && q.id_book == id_book);
            stg.quantity = stg.quantity - quantity;
            db.Entry(stg).State = EntityState.Modified;
            db.SaveChanges();
            return Json(new { con = "success" });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,id_agent,id_book,quantity")] agent_storage agent_storage)
        {
            if (ModelState.IsValid)
            {
                db.agent_storage.Add(agent_storage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_agent = new SelectList(db.agents, "id", "name", agent_storage.id_agent);
            ViewBag.id_book = new SelectList(db.books, "id", "name", agent_storage.id_book);
            return View(agent_storage);
        }

        // GET: agent_storage/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            agent_storage agent_storage = db.agent_storage.Find(id);
            if (agent_storage == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_agent = new SelectList(db.agents, "id", "name", agent_storage.id_agent);
            ViewBag.id_book = new SelectList(db.books, "id", "name", agent_storage.id_book);
            return View(agent_storage);
        }

        // POST: agent_storage/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_agent,id_book,quantity")] agent_storage agent_storage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(agent_storage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_agent = new SelectList(db.agents, "id", "name", agent_storage.id_agent);
            ViewBag.id_book = new SelectList(db.books, "id", "name", agent_storage.id_book);
            return View(agent_storage);
        }

        // GET: agent_storage/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            agent_storage agent_storage = db.agent_storage.Find(id);
            if (agent_storage == null)
            {
                return HttpNotFound();
            }
            return View(agent_storage);
        }

        // POST: agent_storage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            agent_storage agent_storage = db.agent_storage.Find(id);
            db.agent_storage.Remove(agent_storage);
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

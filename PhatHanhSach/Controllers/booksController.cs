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
    public class booksController : Controller
    {
        private PHSEntities db = new PHSEntities();

        // GET: books
        public ActionResult Index()
        {
            var books = db.books.Include(b => b.cat).Include(b => b.publisher);
            return View(books.ToList());
        }

        // GET: books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            book book = db.books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }
        public ActionResult getBook(int book_id)
        {
            var book = db.books.Where(w => w.id == book_id).Select(a => new
            {
                bookname = a.name,
                booknxb = a.publisher.name
            }).First();
            return Json(book, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult getByPub(int pub_id)
        {
            var book = db.books.Where(q=>q.id_nxb == pub_id).Select(a=>new
            {
                bookname = a.name,
                bookid = a.id
            });
            return Json(book,JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult getPriceIn(int? id_book)
        {
            book book = db.books.Find(id_book);
            if (book == null)
            {
                return Json(new { type = "error", value = "Nothing here" });
            }
            return Json(new { price = book.price_in, stock = book.stock });
        }
        [HttpPost]
        public ActionResult getPriceOut(int? id_book)
        {
            book book = db.books.Find(id_book);
            if(book == null)
            {
                return Json(new { type="error",value="Nothing here" });
            }
            return Json(new { price=book.price, stock=book.stock });
        }
        // GET: books/Create
        public ActionResult Create()
        {
            ViewBag.id_cat = new SelectList(db.cats, "id", "name");
            ViewBag.id_nxb = new SelectList(db.publishers, "id", "name");
            return View();
        }

        // POST: books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,id_cat,id_nxb,author,price,price_in,stock")] book book)
        {
            if (ModelState.IsValid)
            {
                db.books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_cat = new SelectList(db.cats, "id", "name", book.id_cat);
            ViewBag.id_nxb = new SelectList(db.publishers, "id", "name", book.id_nxb);
            return View(book);
        }

        // GET: books/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            book book = db.books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_cat = new SelectList(db.cats, "id", "name", book.id_cat);
            ViewBag.id_nxb = new SelectList(db.publishers, "id", "name", book.id_nxb);
            return View(book);
        }

        // POST: books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,id_cat,id_nxb,author,price,price_in,stock")] book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_cat = new SelectList(db.cats, "id", "name", book.id_cat);
            ViewBag.id_nxb = new SelectList(db.publishers, "id", "name", book.id_nxb);
            return View(book);
        }

        // GET: books/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            book book = db.books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            book book = db.books.Find(id);
            db.books.Remove(book);
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

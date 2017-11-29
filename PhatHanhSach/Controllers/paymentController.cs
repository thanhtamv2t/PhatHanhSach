using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhatHanhSach.Models;

namespace PhatHanhSach.Controllers
{
    public class paymentController : Controller
    {
        private PHSEntities db = new PHSEntities();
        // GET: payment
        public ActionResult Index()
        {
            ViewBag.list_nxb = db.publishers.ToList();
            return View();
        }

        public ActionResult getStorage(DateTime? date)
        {
            if(date==null)
            {
                return Json(new { status = "notfound" });
            }
            var num = db.bill_in_detail.Where(w=>w.bill_in.date <= date).GroupBy(q => q.id_book).Select(a => new
            {
                qt = a.Sum(b => b.quantity),
                Name = a.Key
            }).ToList();
            if (num == null) { return Json(new { status = "notfound" }); }
            return Json(num,JsonRequestBehavior.AllowGet);
        }
        public ActionResult getOut(DateTime? date)
        {
            if (date == null)
            {
                return Json(new { status="notfound" });
            }
            var num = db.bill_out_detail.Where(w => w.bill_out.date <= date).GroupBy(q => q.id_book).Select(a => new
            {
                qt = a.Sum(b => b.quantity),
                Name = a.Key
            }).ToList();
            if (num == null) { return Json(new { status = "notfound" }); }
            return Json(num, JsonRequestBehavior.AllowGet);
        }
        public ActionResult getPayment(DateTime from,DateTime to,int pid)
        {
            var bill = db.bill_in.Where(w => w.id_publisher == pid && w.date >= from && w.date <= to && w.status == 0).ToList();
            double total = 0;
            foreach(var item in bill)
            {
                total += item.total;
            }
            return Json(new { total=total },JsonRequestBehavior.AllowGet);

        }
        public ActionResult getBus(DateTime tfrom, DateTime tto)
        {
            var bill_out=db.bill_out_detail.Where(w=>w.bill_out.date >= tfrom && w.bill_out.date <= tto).Sum(a=>a.bill_out.total);
            var bill_in=db.bill_in_detail.Where(w=>w.bill_in.date >= tfrom && w.bill_in.date <=tto).Sum(a=>a.bill_in.total);
            var payment = db.agent_payment_detail.Where(w => w.agent_payment.date >= tfrom && w.agent_payment.date <= tto).Sum(a=>a.agent_payment.amount);
            double bus = payment - bill_in;

            if (bus == null) bus = 0;
            return Json(new { bus=bus }, JsonRequestBehavior.AllowGet);

        }
        // GET: payment/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: payment/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: payment/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: payment/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: payment/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: payment/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: payment/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

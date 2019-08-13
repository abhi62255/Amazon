using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Amazon;

namespace Amazon.Controllers
{
    public class AdminProductTrendController : Controller
    {
        private XKARTEntities db = new XKARTEntities();

        // GET: AdminProductTrend
        public ActionResult Index()
        {
            var tRENDS = db.TRENDS.Include(t => t.PRODUCT);
            return View(tRENDS.ToList());
        }

        // GET: AdminProductTrend/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TREND tREND = db.TRENDS.Find(id);
            if (tREND == null)
            {
                return HttpNotFound();
            }
            return View(tREND);
        }

        // GET: AdminProductTrend/Create
        public ActionResult Create()
        {

            var product = db.PRODUCTs.ToList();
            var TProductId = db.TRENDS.Select(r => r.ProductId).ToList();
            //{
            //    new SelectList(db.PRODUCTs.Where(p => p.ProductId != TProductId), "ProductId", "ProductName");
            //}

            
                foreach (var i in TProductId)
                {
                var pro = db.PRODUCTs.Where(p => p.ProductId == i).FirstOrDefault();
                    product.Remove(pro);
                    
                }
            
            ViewBag.ProductId= new SelectList(product, "ProductId", "ProductName");


            //var k = new SelectList(db.PRODUCTs, "ProductId", "ProductName");


            return View();
        }

        // POST: AdminProductTrend/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TrendId,ProductId")] TREND tREND)
        {
            if (ModelState.IsValid)
            {
                db.TRENDS.Add(tREND);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductId = new SelectList(db.PRODUCTs, "ProductId", "ProductName", tREND.ProductId);
            return View(tREND);
        }

        // GET: AdminProductTrend/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TREND tREND = db.TRENDS.Find(id);
            if (tREND == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(db.PRODUCTs, "ProductId", "ProductName", tREND.ProductId);
            return View(tREND);
        }

        // POST: AdminProductTrend/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TrendId,ProductId")] TREND tREND)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tREND).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(db.PRODUCTs, "ProductId", "ProductName", tREND.ProductId);
            return View(tREND);
        }

        // GET: AdminProductTrend/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TREND tREND = db.TRENDS.Find(id);
            if (tREND == null)
            {
                return HttpNotFound();
            }
            return View(tREND);
        }

        // POST: AdminProductTrend/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TREND tREND = db.TRENDS.Find(id);
            db.TRENDS.Remove(tREND);
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

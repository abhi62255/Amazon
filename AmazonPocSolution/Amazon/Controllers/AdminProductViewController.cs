﻿using System;
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
    [Authorize(Users = "admin_123456")]
    public class AdminProductViewController : Controller
    {
        private XKARTEntities db = new XKARTEntities();

        // GET: AdminProductView
        public ActionResult Index(string searchTerm=null)
        {
            var pRODUCTs = db.PRODUCTs.Where(p => searchTerm == null || p.ProductName.StartsWith(searchTerm))
                .Include(p => p.SELLER);
            return View(pRODUCTs.ToList());
        }


        // GET: AdminProductView/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCT pRODUCT = db.PRODUCTs.Find(id);
            if (pRODUCT == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductSellerId = new SelectList(db.SELLERs, "SellerId", "SellerName", pRODUCT.ProductSellerId);
            return View(pRODUCT);
        }

        // POST: AdminProductView/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,ProductName,ProductPrice,ProductDiscount,ProductQuantity,ProductSellerId")] PRODUCT pRODUCT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pRODUCT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductSellerId = new SelectList(db.SELLERs, "SellerId", "SellerName", pRODUCT.ProductSellerId);
            return View(pRODUCT);
        }

        // GET: AdminProductView/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCT pRODUCT = db.PRODUCTs.Find(id);
            if (pRODUCT == null)
            {
                return HttpNotFound();
            }
            return View(pRODUCT);
        }

        // POST: AdminProductView/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            PRODUCT product = db.PRODUCTs.Find(id);
            PRODUCTDESCRIPTION productDescrption = db.PRODUCTDESCRIPTIONs.Where(d => d.ProductId == id).FirstOrDefault();
            if(productDescrption != null)
            {
                db.PRODUCTDESCRIPTIONs.Remove(productDescrption);
            }
            db.PRODUCTs.Remove(product);
         
            while (true)
            {
                PRODUCTPICTURE productPicture = db.PRODUCTPICTUREs.Where(d => d.ProductId == id).FirstOrDefault();
                if(productPicture is null)
                {
                    break;
                }
                db.PRODUCTPICTUREs.Remove(productPicture);
                db.SaveChanges();
            }
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

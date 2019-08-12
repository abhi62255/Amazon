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
    [Authorize(Users = "admin_123456")]
    public class AdminSellerController : Controller
    {
        private XKARTEntities db = new XKARTEntities();

        // GET: AdminSeller
        public ActionResult Index(string searchTerm = null)
        {
            var sELLERs = db.SELLERs.Where(p => searchTerm == null || p.SellerName.StartsWith(searchTerm))
                .Include(s => s.ADDRESS);
            return View(sELLERs.ToList());
        }

      

        // GET: AdminSeller/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SELLER sELLER = db.SELLERs.Find(id);
            if (sELLER == null)
            {
                return HttpNotFound();
            }
            ViewBag.SellerAddressId = new SelectList(db.ADDRESSes, "AddressId", "AddressLine1", sELLER.SellerAddressId);
            return View(sELLER);
        }

        // POST: AdminSeller/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SellerId,SellerName,SellerAddressId")] SELLER sELLER)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sELLER).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SellerAddressId = new SelectList(db.ADDRESSes, "AddressId", "AddressLine1", sELLER.SellerAddressId);
            return View(sELLER);
        }

        // GET: AdminSeller/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SELLER sELLER = db.SELLERs.Find(id);
            if (sELLER == null)
            {
                return HttpNotFound();
            }
            return View(sELLER);
        }

        // POST: AdminSeller/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            SELLER seller = db.SELLERs.Find(id);

            while (true) //To delete the product related to the seller
            {
                var product = db.PRODUCTs.Where(p => p.ProductSellerId == seller.SellerId).FirstOrDefault();
                if (product == null)
                {
                    break;
                }
                //return Redirect("/AdminProductView/Delete/" + product.ProductId);
                PRODUCTDESCRIPTION productDescrption = db.PRODUCTDESCRIPTIONs.Where(d => d.ProductId == product.ProductId).FirstOrDefault();
                if (productDescrption != null)
                {
                    db.PRODUCTDESCRIPTIONs.Remove(productDescrption);
                }

                while (true)
                {
                    PRODUCTPICTURE productPicture = db.PRODUCTPICTUREs.Where(d => d.ProductId == product.ProductId).FirstOrDefault();
                    if (productPicture == null)
                    {
                        break;
                    }
                    db.PRODUCTPICTUREs.Remove(productPicture);
                    db.SaveChanges();

                }
                db.PRODUCTs.Remove(product);
                db.SaveChanges();
                //finish

            }
            
            var count = db.SELLERs.Where(a => a.SellerAddressId == seller.SellerAddressId).Count(); //To check if the multiple seller beelong to that address
            if (count == 1)    //if only on seller belong to that address  then delete the address with the seller
            {
                ADDRESS address = db.ADDRESSes.Where(a => a.AddressId == seller.SellerAddressId).FirstOrDefault();
                db.ADDRESSes.Remove(address);
            }
            db.SELLERs.Remove(seller);
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

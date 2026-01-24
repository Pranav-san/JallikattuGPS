using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Jallikattu.Models;

namespace Jallikattu.Controllers
{
    public class ProductsController : Controller
    {
        private JallikattuGPSEntities db = new JallikattuGPSEntities();

        // GET: Products
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }



        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,ProductName,Price,Description,Stock,ImageURL,ImageURL2,ImageURL3 FeatureImageURL,FeatureImageURL2,FeatureImageURL3, FeatureImageURL4, " +
            "FeatureDescription, FeatureDescription2, FeatureDescription3, FeatureDescription4, CreatedBy,CreatedAt,UpdatedBy,UpdatedAt")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                product.CreatedAt = DateTime.Now;
                product.UpdatedAt = DateTime.Now;

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            if (!ModelState.IsValid)
                return View(product);

            var productInDB = db.Products.Find(product.ProductID);

            if (productInDB == null)
                return HttpNotFound();

            productInDB.ProductName = product.ProductName;
            productInDB.ImageURL = product.ImageURL;
            productInDB.ImageURL2 = product.ImageURL2;
            productInDB.ImageURL3 = product.ImageURL3;

            productInDB.FeatureImageURL = product.FeatureImageURL;
            productInDB.FeatureDescription = product.FeatureDescription;
            productInDB.FeatureImageURL2 = product.FeatureImageURL2;
            productInDB.FeatureDescription2 = product.FeatureDescription2;
            productInDB.FeatureImageURL3 = product.FeatureImageURL3;
            productInDB.FeatureDescription3 = product.FeatureDescription3;
            productInDB.FeatureImageURL4 = product.FeatureImageURL4;
            productInDB.FeatureDescription4 = product.FeatureDescription4;

            productInDB.Description = product.Description;
            productInDB.Price = product.Price;
            productInDB.Stock = product.Stock;

            productInDB.UpdatedAt = DateTime.Now;

            db.SaveChanges();

            return RedirectToAction("Index");


        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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

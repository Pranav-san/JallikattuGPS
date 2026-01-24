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
    public class BlogController : Controller
    {
        private JallikattuGPSEntities db = new JallikattuGPSEntities();

        // GET: Blog
        public ActionResult Index()
        {
            return View(db.BlogTables.ToList());
        }

        // GET: Blog/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogTable blogTable = db.BlogTables.Find(id);
            if (blogTable == null)
            {
                return HttpNotFound();
            }
            return View(blogTable);
        }

        // GET: Blog/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Blog/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PostID,BlogTitle,ImgURL,BlogContent,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt")] BlogTable blogTable)
        {
            if (ModelState.IsValid)
            {
                db.BlogTables.Add(blogTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(blogTable);
        }

        // GET: Blog/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogTable blogTable = db.BlogTables.Find(id);
            if (blogTable == null)
            {
                return HttpNotFound();
            }
            return View(blogTable);
        }

        // POST: Blog/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PostID,BlogTitle,ImgURL,BlogContent,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt")] BlogTable blogTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(blogTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blogTable);
        }

        // GET: Blog/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogTable blogTable = db.BlogTables.Find(id);
            if (blogTable == null)
            {
                return HttpNotFound();
            }
            return View(blogTable);
        }

        // POST: Blog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BlogTable blogTable = db.BlogTables.Find(id);
            db.BlogTables.Remove(blogTable);
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

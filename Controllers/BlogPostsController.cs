using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Jallikattu.Models;

namespace Jallikattu.Controllers
{
    public class BlogPostsController : Controller
    {
        private JallikattuGPSEntities db = new JallikattuGPSEntities();

        // GET: BlogPosts
        public ActionResult Index()
        {
            return View(db.BlogPostsTables.ToList());
        }

        // GET: BlogPosts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPostsTable blogPostsTable = db.BlogPostsTables.Find(id);
            if (blogPostsTable == null)
            {
                return HttpNotFound();
            }
            return View(blogPostsTable);
        }

        // GET: BlogPosts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BlogPosts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PostID,ImageURL,BlogTitle,BlogContent,CreatedBy,CreatedAt,UpdatedBy, Slug, Category")] BlogPostsTable blogPostsTable)
        {
            if (ModelState.IsValid)
            {
                blogPostsTable.CreatedAt = DateTime.Now;
                blogPostsTable.UpdatedAt = DateTime.Now;
                db.BlogPostsTables.Add(blogPostsTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(blogPostsTable);
        }

        // GET: BlogPosts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPostsTable blogPostsTable = db.BlogPostsTables.Find(id);
            if (blogPostsTable == null)
            {
                return HttpNotFound();
            }
            return View(blogPostsTable);
        }

        // POST: BlogPosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BlogPostsTable model)
        {

            if (!ModelState.IsValid)
                return View(model);

            var modelInDB = db.BlogPostsTables.Find(model.PostID);

            if (modelInDB == null)
                return HttpNotFound();

            modelInDB.BlogTitle = model.BlogTitle;
            modelInDB.BlogContent = model.BlogContent;
            modelInDB.ImageURL = model.ImageURL;
            modelInDB.Category = model.Category;
            modelInDB.Slug = model.Slug;

            modelInDB.UpdatedAt = DateTime.Now;

            db.SaveChanges();

            return RedirectToAction("Index");


        }

        // GET: BlogPosts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPostsTable blogPostsTable = db.BlogPostsTables.Find(id);
            if (blogPostsTable == null)
            {
                return HttpNotFound();
            }
            return View(blogPostsTable);
        }

        // POST: BlogPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BlogPostsTable blogPostsTable = db.BlogPostsTables.Find(id);
            db.BlogPostsTables.Remove(blogPostsTable);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        // GET: BlogPosts/PostDetails/5
        public ActionResult PostDetails(int id)
        {
            using (var db = new JallikattuGPSEntities())
            {
                var post = db.BlogPostsTables.FirstOrDefault(x => x.PostID == id);

                return View(post);
            }
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

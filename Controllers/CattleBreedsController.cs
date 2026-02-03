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
    [Authorize (Roles = "Admin")]
    public class CattleBreedsController : Controller
    {
        private Entities db = new Entities();

        // GET: CattleBreeds
        public ActionResult Index()
        {
            var breeds = db.CattleBreedsTables.ToList();
            return View(breeds);
            
        }


        public ActionResult CattleBreedsPanel()
        {
            return View(db.CattleBreedsTables.ToList());
        }

        // GET: CattleBreeds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CattleBreedsTable cattleBreedsTable = db.CattleBreedsTables.Find(id);
            if (cattleBreedsTable == null)
            {
                return HttpNotFound();
            }
            return View(cattleBreedsTable);
        }

        // GET: CattleBreeds/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CattleBreeds/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CattleID,BreedName,Utility,Origin,Distribution,Description,ImageURL,Createdby,CreatedAt,UpdatedBy,UpdatedAt")] CattleBreedsTable cattleBreedsTable)
        {
            if (ModelState.IsValid)
            {
                cattleBreedsTable.CreatedAt = DateTime.Now;
                cattleBreedsTable.UpdatedAt = DateTime.Now;
                db.CattleBreedsTables.Add(cattleBreedsTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cattleBreedsTable);
        }

        // GET: CattleBreeds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CattleBreedsTable cattleBreedsTable = db.CattleBreedsTables.Find(id);
            if (cattleBreedsTable == null)
            {
                return HttpNotFound();
            }
            return View(cattleBreedsTable);
        }

        // POST: CattleBreeds/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CattleBreedsTable cattleBreed)
        {
            if (!ModelState.IsValid)
                return View(cattleBreed);

            var cattleBreedInDB = db.CattleBreedsTables
                                    .Find(cattleBreed.CattleID);

            if (cattleBreedInDB == null)
                return HttpNotFound();

            cattleBreedInDB.ImageURL = cattleBreed.ImageURL;
            cattleBreedInDB.BreedName = cattleBreed.BreedName;
            cattleBreedInDB.Description = cattleBreed.Description;
            cattleBreedInDB.Distribution = cattleBreed.Distribution;
            cattleBreedInDB.Origin = cattleBreed.Origin;
            cattleBreedInDB.Utility = cattleBreed.Utility;

            cattleBreedInDB.UpdatedAt = DateTime.Now;

            db.SaveChanges();

            return RedirectToAction("CattleBreedsPanel");
        }


        // GET: CattleBreeds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CattleBreedsTable cattleBreedsTable = db.CattleBreedsTables.Find(id);
            if (cattleBreedsTable == null)
            {
                return HttpNotFound();
            }
            return View(cattleBreedsTable);
        }

        // POST: CattleBreeds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CattleBreedsTable cattleBreedsTable = db.CattleBreedsTables.Find(id);
            db.CattleBreedsTables.Remove(cattleBreedsTable);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: CattleInfo
        

        [AllowAnonymous]
        public ActionResult CattleDetails(int id)
        {
            using (var db = new Entities())
            {
                var breed = db.CattleBreedsTables.FirstOrDefault(x => x.CattleID == id);

                return View(breed);
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

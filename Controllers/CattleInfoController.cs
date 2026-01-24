using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Jallikattu.Models;
using Jallikattu.Models;


namespace Jallikattu.Controllers
{
    public class CattleInfoController : Controller
    {
        // GET: CattleInfo
        public ActionResult Index()
        {
            using (var db = new JallikattuGPSEntities())
            {
                var breeds = db.CattleBreedsTables.ToList();
                return View(breeds);
            }
        }

        public ActionResult CattleInfo()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            using (var db = new JallikattuGPSEntities())
            {
                var breed = db.CattleBreedsTables
                              .FirstOrDefault(x => x.CattleID == id);

                return View(breed);
            }
        }
    }
}
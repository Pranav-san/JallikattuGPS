using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Jallikattu.Models;

namespace Jallikattu.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (var db = new JallikattuGPSEntities())
            {
                var breeds = db.CattleBreedsTables.Take(3).ToList();

                ViewBag.products = db.Products.Take(3).ToList();

                ViewBag.JallikattuTypes = db.BlogPostsTables.Where(b => b.Category== "JallikattuType").ToList();
                ViewBag.BlogPosts = db.BlogPostsTables.Take(3).ToList();



                return View(breeds);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Blog()
        {
            using (var db = new JallikattuGPSEntities())
            {
                var blogPosts = db.BlogPostsTables.ToList();
                return View(blogPosts);
            }


        }

        public ActionResult Events()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult CattleTrackingSystem()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult CattleBreeds()
        {
            using (var db = new JallikattuGPSEntities())
            {
                var breeds = db.CattleBreedsTables.ToList();
                return View(breeds);
            }
        }

        public ActionResult Products()
        {
            using (var db = new JallikattuGPSEntities())
            {
                var products = db.Products.ToList();
                return View(products);
            }

        }

        public ActionResult AdminDashBoard()
        {

            return View();
        }

        public ActionResult UserDashBoard()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult GetEvents()
        {
            using (var db = new JallikattuGPSEntities())
            {
                var eventsFromDb = db.EventsTables.Where(e => e.Status==1).Select(e => new
                {
                    e.EventName,
                    e.EventLocation,
                    e.EventDate
                }).ToList();

                var events = eventsFromDb.Select(e => new
                {
                    title = e.EventName + " - " + e.EventLocation,
                    start = e.EventDate.ToString("yyyy-MM-dd"),
                    location = e.EventLocation
                }).ToList();

                return Json(events, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult AddEvent()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddEvent(string UserName, string MobileNo, string EventName, string EventLocation, DateTime EventDate,
            int CaptchaAnswer, int CaptchaExpected)
        {


            if (CaptchaAnswer != CaptchaExpected)
            {
                ModelState.AddModelError("", "Captcha answer is incorrect.");
            }

            if (string.IsNullOrWhiteSpace(MobileNo) || MobileNo.Length != 10 || !MobileNo.All(char.IsDigit))
            {
                ModelState.AddModelError("", "Enter a valid 10-digit mobile number.");
            }

            if (!string.IsNullOrEmpty(UserName) && UserName.Length > 70)
            {
                ModelState.AddModelError("", "Name must not exceed 70 characters.");
            }

            //If any error return same page
            if (!ModelState.IsValid)
            {
                return View();
            }


            using (var db = new JallikattuGPSEntities())
            {
                EventsTable ev = new EventsTable
                {
                    UserName = UserName,
                    MobileNo = MobileNo,
                    EventName = EventName,
                    EventLocation = EventLocation,
                    EventDate = EventDate,

                    Status = 0,

                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    CreatedBy = 100,
                    UpdatedBy = 100
                };

                db.EventsTables.Add(ev);
                db.SaveChanges();
            }




            // Store success message
            TempData["EventSubmitted"] = "Event submitted for approval";

            // Redirect to calendar section
            return Redirect(Url.Action("Index", "Home") + "#View-Calendar");
        }




    }

   
}
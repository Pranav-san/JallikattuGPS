using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Jallikattu.Models;

namespace Jallikattu.Controllers
{
    public class BlogInfoController : Controller
    {
        //GET: BlogInfo
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PostDetails(int id)
        {
            using (var db = new JallikattuGPSEntities())
            {
                var post = db.BlogPostsTables.FirstOrDefault(x => x.PostID == id);

                return View(post);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Jallikattu.Models;

namespace Jallikattu.Controllers
{
    public class ProductInfoController : Controller
    {
        // GET: ProductInfo
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductDetails(int id)
        {
            using (var db = new JallikattuGPSEntities())
            {
                var product = db.Products
                              .FirstOrDefault(x => x.ProductID == id);

                return View(product);
            }
        }
    }
}
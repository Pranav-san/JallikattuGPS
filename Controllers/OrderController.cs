using Jallikattu.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jallikattu.Controllers
{
    public class OrderController : Controller
    {
        private JallikattuGPSEntities db = new JallikattuGPSEntities();

        private List<CartItem> GetCart()
        {
            if (Session["Cart"] == null)
                Session["Cart"] = new List<CartItem>();

            return (List<CartItem>)Session["Cart"];
        }

        public ActionResult Index()
        {
            return View(GetCart());
        }

        public ActionResult AddToCart(int id)
        {
            var product = db.Products.Find(id);
            if (product == null)
                return HttpNotFound();

            var cart = GetCart();
            var existingItem = cart.FirstOrDefault(x => x.ProductID == id);

            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                cart.Add(new CartItem
                {
                    ProductID = product.ProductID,
                    ProductName = product.ProductName,
                    Price = product.Price,
                    Quantity = 1,
                    ImageURL = product.ImageURL
                });
            }

            Session["Cart"] = cart;
            return RedirectToAction("Index");
        }

        public ActionResult Remove(int id)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(x => x.ProductID == id);
            if (item != null)
                cart.Remove(item);

            return RedirectToAction("Index");
        }

        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = GetCart();
            int count = cart.Sum(x => x.Quantity);
            return PartialView("_CartSummary", count);
        }

        [Authorize]
        public ActionResult Checkout()
        {
            var cart = GetCart();

            if (!cart.Any())
                return RedirectToAction("Index");

            var order = new Order
            {
                UserId = User.Identity.GetUserId(),
                CreatedAt = DateTime.Now,
                OrderStatus = "Pending",
                TotalAmount = cart.Sum(x => x.Price * x.Quantity)
            };

            foreach (var item in cart)
            {
                db.OrderItems.Add(new OrderItem
                {
                    OrderID = order.OrderID,
                    ProductID = item.ProductID,
                    UnitPrice = item.Price,
                    Quantity = item.Quantity
                });
            }

            db.SaveChanges();

            // Store order id temporarily
            Session["CurrentOrderId"] = order.OrderID;

            return View();
        }
    }
}
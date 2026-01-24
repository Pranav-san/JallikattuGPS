using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Jallikattu.Models;

namespace Jallikattu.Controllers
{
    public class AuthController : Controller
    {

        private JallikattuGPSEntities db = new JallikattuGPSEntities();


        // GET: Auth
        public ActionResult Register()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserTable user)
        {
            if (!ModelState.IsValid)
                return View(user);

            var existingUser = db.UserTables.FirstOrDefault(u => u.Email == user.Email);

            if (existingUser != null)
            {
                ModelState.AddModelError("Email", "Email already registered");
                return View(user);
            }

            // PASSWORD
            user.Password = Crypto.HashPassword(user.Password);

            user.Role = "User";
            user.CreatedAt = DateTime.Now;

            db.UserTables.Add(user);
            db.SaveChanges();

            return RedirectToAction("Login");
        }


        //Login
        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string Email, string Password)
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                ViewBag.Error = "Email and Password are required";
                return View();
            }

            var user = db.UserTables.FirstOrDefault(u => u.Email == Email);

            if (user == null || !Crypto.VerifyHashedPassword(user.Password, Password))
            {
                ViewBag.Error = "Invalid email or password";
                return View();
            }


            Session["UserID"] = user.UserID;
            Session["UserName"] = user.UserName;
            Session["Role"] = user.Role;


            if (user.Role == "Admin")
                return RedirectToAction("AdminDashBoard", "Home");

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }





    }
}
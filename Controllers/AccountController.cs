using Jallikattu.Data.ViewModels;
using Jallikattu.Models.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Jallikattu.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser, string> _signInManager;

        private UserManager<ApplicationUser> UserManager
            => _userManager ?? HttpContext.GetOwinContext().GetUserManager<UserManager<ApplicationUser>>();

        private SignInManager<ApplicationUser, string> SignInManager
            => _signInManager ?? HttpContext.GetOwinContext().Get<SignInManager<ApplicationUser, string>>();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var existingUser = await UserManager.FindByEmailAsync(model.Email);

            if (existingUser == null)
            {
                ModelState.AddModelError("Email", "Invalid Credentials");
                return View(model);
            }

            var result = await SignInManager.PasswordSignInAsync(
                model.Email,
                model.Password,
                isPersistent: false,
                shouldLockout: false
            );

            if (result == SignInStatus.Success)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Invalid login attempt");
            return View(model);
        }

        [HttpPost]
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var existingUser = await UserManager.FindByEmailAsync(model.Email);

            if (existingUser != null)
            {
                ModelState.AddModelError("Email","An account with this email already exists.");
                return View(model);
            }

            var user = new ApplicationUser
            {
                FullName = model.FullName,
                UserName = model.Email,
                Email = model.Email
            };
            var result = await UserManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                //Assign default role
                await UserManager.AddToRoleAsync(user.Id, "User");

                // Auto-login after registration
                await SignInManager.SignInAsync(
                    user,
                    isPersistent: false,
                    rememberBrowser: false
                );

                return RedirectToAction("Index", "Home");
            }

            //Show Identity validation errors
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }

            return View(model);
        }
    }
}
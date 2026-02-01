using Jallikattu.Models;
using Jallikattu.Models.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace Jallikattu
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<UserManager<ApplicationUser>>(CreateUserManager);
            app.CreatePerOwinContext<SignInManager<ApplicationUser, string>>(CreateSignInManager);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });
        }

        private static UserManager<ApplicationUser> CreateUserManager(
            IdentityFactoryOptions<UserManager<ApplicationUser>> options,
            IOwinContext context)
        {
            var manager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));

            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false
            };

            return manager;
        }

        private static SignInManager<ApplicationUser, string> CreateSignInManager(
            IdentityFactoryOptions<SignInManager<ApplicationUser, string>> options,
            IOwinContext context)
        {
            return new SignInManager<ApplicationUser, string>(
                context.GetUserManager<UserManager<ApplicationUser>>(),
                context.Authentication);
        }

        private void CreateRolesAndAdmin()
        {
            using (var context = new ApplicationDbContext())
            {
                var roleManager = new RoleManager<IdentityRole>(
                    new RoleStore<IdentityRole>(context));

                if (!roleManager.RoleExists("Admin"))
                    roleManager.Create(new IdentityRole("Admin"));

                if (!roleManager.RoleExists("User"))
                    roleManager.Create(new IdentityRole("User"));
            }
        }

        private void CreateTestUser()
        {
            using (var context = new ApplicationDbContext())
            {
                var userManager = new UserManager<ApplicationUser>(
                    new UserStore<ApplicationUser>(context));

                var user = userManager.FindByEmail("admin@test.com");

                if (user == null)
                {
                    user = new ApplicationUser
                    {
                        UserName = "admin@test.com",
                        Email = "admin@test.com"
                    };

                    userManager.Create(user, "Admin@123");
                    userManager.AddToRole(user.Id, "Admin");
                }
            }
        }
    }
}

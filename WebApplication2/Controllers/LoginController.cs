using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Security.Claims;
using WebApplication2.ViewModels;

namespace WebApplication2.Controllers
{
    [Route("login")]
    public class LoginController : Controller
    {
        // Dependency injection
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        public LoginController(UserManager<IdentityUser> userManager,
                               SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        // Login Methods
        [Route("")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //var user = new IdentityUser { UserName = "Admin" };
            //var result = await userManager.CreateAsync(user, "Vadim123");
            //result = userManager.AddToRoleAsync()

            if (User.Identity is not null && User.Identity.IsAuthenticated)
            {
                return await ManageRedirecting(await userManager.GetUserAsync(User));
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Login,
                                                                     model.Password,
                                                                     model.RememberMe,
                                                                     false);

                if (result.Succeeded)
                {
                    return await ManageRedirecting(await userManager.GetUserAsync(User));
                }

                ModelState.AddModelError(string.Empty, "Ошибка входа: неверные данные");
            }

            return View(model);
        }

        private async Task<RedirectToActionResult> ManageRedirecting(IdentityUser user)
        {
            string? controller = null;
            string? action = null;

            if (await userManager.IsInRoleAsync(user, "admin"))
            {
                controller = "admin";
                action = "readdepartment";
            }

            if (await userManager.IsInRoleAsync(user, "employee"))
            {
                controller = "employee";
                action = "tasks";
            }

            if (await userManager.IsInRoleAsync(user, "boss"))
            {
                controller = "boss";
                action = "manage";
            }

            return RedirectToAction(action, controller);
        }
    }
}

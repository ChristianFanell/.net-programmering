using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LabbUppgift3.Infrastructure;
using LabbUppgift3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LabbUppgift3.Controllers
{
    // start och loginsida
    //[Authorize]
    public class HomeController : Controller
    {

        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;


        public HomeController(UserManager<IdentityUser> userMgr, SignInManager<IdentityUser> signInMgr)
        {
            userManager = userMgr;
            signInManager = signInMgr;
        }


        // GET: /<controller>/

        public ViewResult Index()
        {
            var errand = HttpContext.Session.Getjson<Errand>("NewErrand");

            // returnerar vy utan data om ej session
            if (errand == null)
            {
                return View();
            }

            // returnerar vy med sessionsdata från tidigare formulärifyllning
            else
            {
                return View(errand);
            }
            
        }


        public ViewResult Login(string returnurl)
        {
            return View(new LoginModel
            {
                ReturnUrl = returnurl
            });               
              
        }

        // här loggas användaren in och redirectas om allt är som det ska med hänsyn till roller
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login (LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await userManager.FindByNameAsync(loginModel.UserName);

                if (user != null)
                {
                    await signInManager.SignOutAsync();

                    if ((await signInManager.PasswordSignInAsync(user, loginModel.Password, false, false)).Succeeded)
                    {
                        if (await userManager.IsInRoleAsync(user, "Coordinator"))
                        { 
                            return Redirect(loginModel?.ReturnUrl ?? "/Coordinator/StartCoordinator");
                        }
                        if (await userManager.IsInRoleAsync(user, "Investigator"))
                        {
                            return Redirect(loginModel?.ReturnUrl ?? "/Investigator/StartInvestigator");
                        }
                        if (await userManager.IsInRoleAsync(user, "Manager"))
                        {
                            return Redirect(loginModel?.ReturnUrl ?? "/Manager/StartManager");
                        }
                    }
                }
            }
            ModelState.AddModelError("", "Felaktigt användarnamn eller lösenord");
            return View(loginModel);
        }

        public async Task<RedirectResult> Logout(string returnUrl = "/")
        {
            await signInManager.SignOutAsync();
            return Redirect(returnUrl);
        }

        // om man klickar på en länk man inte får gå in på
        public ViewResult AccessDenied()
        {
            return View();
        }


    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using TownWebApp.Models;
using TownWebApp.ViewModels.AccountViewModels;

namespace TownWebApp.Controllers
{
    public class AccountController : Controller
    {   
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

       

        public async Task<IActionResult> Register()
       {
            return View();
       }
        [HttpPost]
        public async Task<IActionResult>Register(RegisterVM register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }
            AppUser user=new AppUser()
            {
                Email = register.Email,
                UserName = register.UserName,
                Name=register.Name,
                Surname = register.Surname,
            };
            IdentityResult identityResult=await _userManager.CreateAsync(user,register.Password);
            if (!identityResult.Succeeded)
            {
                foreach(var error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }
            return RedirectToAction("Login");
        }
       public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>Login(LoginVM login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }
            AppUser user=await _userManager.FindByEmailAsync(login.Email);
            if (user == null)
            {
                return View(user);
            }
            Microsoft.AspNetCore.Identity.SignInResult signInResult = await _signInManager.PasswordSignInAsync(user, login.Password, true, false);
            if (!signInResult.Succeeded)
            {
                ModelState.AddModelError("", "password or email incorrect");
                return View();
            }
            return RedirectToAction("Index","Home");


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mic.Volo.MOBILE_ST.Data.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Mic.Volo.MOBILE_ST.Controllers
{

    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        //public IActionResult Login([FromQuery] string returnUrl)
        //{
        //    return View(new LoginViewModel {
        //        ReturnUrl=returnUrl
        //    });
        //}
        //[HttpPost]
        //[AllowAnonymous]
        //public async Task<IActionResult> Login([FromForm] LoginViewModel loginViewModel)
        //{
        //    if(!ModelState.IsValid)
        //    {
        //        return View(loginViewModel);
        //    }
        //    IdentityUser user = null;
        //    user = await _userManager.FindByEmailAsync(loginViewModel.EmailOrUsername);
        //    //if(loginViewModel.EmailOrUsername.Contains("@"))
        //    //{
        //    //    user = await _userManager.FindByEmailAsync(loginViewModel.EmailOrUsername);
        //    //}
        //    //else
        //    //{
        //    //    user = await _userManager.FindByEmailAsync(loginViewModel.EmailOrUsername);
        //    //}
        //    if(user!=null)
        //    {
        //        var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
        //        if(result.Succeeded)
        //        {
        //            return Redirect(loginViewModel.ReturnUrl ?? "/");
        //        }
        //    }
        //    ModelState.AddModelError("", "Invalid Username or Password");
        //    return View(loginViewModel);
        //}


        //***************************

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Phones");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect login password!");
                }
            }
            return View(model);
        }

        

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> Register([FromForm]RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { Email = model.Email, UserName = model.UserName };
                
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                  
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Phones");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }








        //[HttpPost]
        //[AllowAnonymous]
        //public async Task<IActionResult> Register([FromForm]RegisterViewModel registerViewModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(registerViewModel);
        //    }

        //    var user = new IdentityUser
        //    {
        //        Email = registerViewModel.Email,
        //        UserName = registerViewModel.UserName
        //    };

        //    var result = await _userManager.CreateAsync(user, registerViewModel.Password);
        //    if (result.Succeeded)
        //    {
        //        return await Login(new LoginViewModel
        //        {
        //            EmailOrUsername = registerViewModel.Email,
        //            Password = registerViewModel.Password,
        //            ReturnUrl = registerViewModel.ReturnUrl
        //        });

        //    }

        //    foreach (var error in result.Errors)
        //    {
        //        ModelState.AddModelError("", error.Description);
        //    }

        //    return View(registerViewModel);
        //}

        //[HttpPost]
        //[Authorize]
        //public async Task<IActionResult> Logout()
        //{
        //    await _signInManager.SignOutAsync();
        //    return Redirect("/");
        //}

        //public async Task<IActionResult> SignOut()
        //{
        //    await Logout();
        //    return RedirectToAction("Login");
        //}
    }
}
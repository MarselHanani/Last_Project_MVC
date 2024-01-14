using Booky.ADL.Models;
using Booky.PL.Helper;
using Booky.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Booky.PL.Areas.Security.Controllers;

    [Area("Security")]
public class SecurityController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public SecurityController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViews model)
    {
        if (ModelState.IsValid)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email.Split('@')[0],
                Fname = model.Fname,
                Lname = model.Lname,
                Email = model.Email,
                isAgree = model.isAgree
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Login");
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

    [HttpGet]
    public IActionResult Login()
    {
        return View();
        
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViews model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var flag = await _userManager.CheckPasswordAsync(user, model.Password);
                if (flag)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home", new { area = "Customer" });
                    }
                }
            }
            TempData["Error"] = "Wrong Email or Password";
        }
        return View(model);
    }
    [HttpGet]
    public async Task<IActionResult> SingOut()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login");
        
    }
    [HttpGet]
    
    
    public IActionResult SendEmail()
    {
        return View();
    }
    [HttpPost]

    public async Task<IActionResult> SendEmail(ForgetPasswordViews model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var linkResetPassword = Url.Action("ResetPassword", "Security", new { email = model.Email,token }, Request.Scheme);
                var email = new Email()
                {
                    Subject = "Reset Password",
                    To = model.Email,
                    Body = linkResetPassword
                };
                EmailManagement.SendEmail(email);
                return RedirectToAction("CheckInbox");
            }
        }  
        return View(model);
    }

    public IActionResult CheckInbox()
    {
        return View();
        
    }
    [HttpGet]
    public IActionResult ResetPassword(string email, string token)
    {
        TempData["email"] = email;
        TempData["token"] = token;
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> ResetPassword(ResetPasswordViews model)
    {
        if (ModelState.IsValid)
        {
            var email = TempData["email"] as string;
            var token = TempData["token"] as string; 
            var user = await _userManager.FindByEmailAsync(email);
            var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);
            if (result.Succeeded)
            {
                return RedirectToAction("Login");
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
}
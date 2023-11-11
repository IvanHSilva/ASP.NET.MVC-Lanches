﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VendasLanches.ViewModels;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace VendasLanches.Controllers;

public class AccountController : Controller {
    
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AccountController(UserManager<IdentityUser> userManager, 
        SignInManager<IdentityUser> signInManager) {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpGet]
    public IActionResult Login(string returnUrl) {
        return View(new LoginViewModel(){
            ReturnUrl = returnUrl
        });
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginVM) {

        if (!ModelState.IsValid) return View(loginVM);

        IdentityUser user = await _userManager.FindByNameAsync(loginVM.UserName);

        if (user != null) {

            SignInResult result = await _signInManager.PasswordSignInAsync(
                user, loginVM.Password, false, false);
            if (result.Succeeded) {
                if (string.IsNullOrEmpty(loginVM.ReturnUrl)) {
                    return RedirectToAction("Index", "Home");
                }
                return Redirect(loginVM.ReturnUrl);
            }
        }
        ModelState.AddModelError("", "Falha ao tentar fazer o login!!");
        return View(loginVM);
    }

    [HttpGet]
    public IActionResult Register() {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(LoginViewModel registerVM) {

        if (ModelState.IsValid) {
            IdentityUser user = new IdentityUser { UserName = registerVM.UserName };
            IdentityResult result = await _userManager.CreateAsync(user, registerVM.Password);

            if (result.Succeeded) {
                // await _signInManager.SignInAsync(user, isPersistent: false);
                await _userManager.AddToRoleAsync(user, "Member");
                return RedirectToAction("Login", "Account");
            } else {
                this.ModelState.AddModelError("Registro", "Falha ao registrar o usuário");
            }
        }

        return View(registerVM);
    }

    [HttpPost]
    public async Task<IActionResult> Logout() {
        HttpContext.Session.Clear();
        HttpContext.User = null!;
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}

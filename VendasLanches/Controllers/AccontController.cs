using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VendasLanches.ViewModels;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace VendasLanches.Controllers;

public class AccontController : Controller {
    
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AccontController(UserManager<IdentityUser> userManager, 
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
}

using Book_Store.Models;
using Book_Store.Models.ViewModels;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Book_Store.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<AppCustomer> _userManager;
        private SignInManager<AppCustomer> _signInManager;
        public AccountController(SignInManager<AppCustomer> signInManager, UserManager<AppCustomer> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

		public IActionResult Register() => View();
        
        [HttpPost]
        public async Task<IActionResult> Register(Customer cust)
        {
            if (ModelState.IsValid)
            {
                AppCustomer newCustomer = new AppCustomer
                {
                    CustomerName = cust.CustomerName,
                    Birth = cust.Birth,
                    Gender = cust.Gender,
                    PhoneNumber = cust.Phone,
                    Account = cust.Account,
                    UserName = cust.Account,
                    Email = cust.Email,
                    Address = cust.Address
                };
                IdentityResult result = await _userManager.CreateAsync(newCustomer, cust.Password);

                if (result.Succeeded)
                {
                    return Redirect("/");
                }
                foreach (IdentityError err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
            }
            return View(cust);
        }

        public IActionResult SignIn(string returnUrl ) => View(new SignInViewModel { returnURL = returnUrl });

        [HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> SignIn(SignInViewModel signinVM, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(signinVM.Account, signinVM.Password, false, false);

                if (result.Succeeded)
                {
                    return Redirect(signinVM.returnURL = "/" ?? "/");
                }

                ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng");
            }

            return View(signinVM);
        }

        public async Task<IActionResult> Logout(string returnUrl = "/")
        {
            await _signInManager.SignOutAsync();
            return Redirect(returnUrl);
        }
    }
}

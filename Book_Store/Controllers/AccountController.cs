using Book_Store.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Threading.Tasks;

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

        public IActionResult Register()
        {
            return View();
        }

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
					UserName = cust.Account,
					Email = cust.Email,
					Address = cust.Address
				};
				IdentityResult result = await _userManager.CreateAsync(newCustomer);

				if (result.Succeeded) {
					return Redirect("/Account");
				}
				foreach (IdentityError err in result.Errors)
				{
					ModelState.AddModelError("", err.Description);
				}
			}
			return View(cust);
		}

		public IActionResult SignIn()
		{
			return View();
		}
	}
}

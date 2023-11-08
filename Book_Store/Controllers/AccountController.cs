using Microsoft.AspNetCore.Mvc;

namespace Book_Store.Controllers
{
	public class AccountController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}

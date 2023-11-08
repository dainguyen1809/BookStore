using Microsoft.AspNetCore.Mvc;

namespace Book_Store.Controllers
{
	public class CartController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}

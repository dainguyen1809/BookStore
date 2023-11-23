using Microsoft.AspNetCore.Mvc;

namespace BookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
		public IActionResult Index() => View();
	}
}

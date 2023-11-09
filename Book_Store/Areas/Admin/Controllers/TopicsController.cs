using Book_Store.Models;
using Book_Store.Repository.Data;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TopicsController : Controller
    {
        private readonly DataContext _db;

        public TopicsController(DataContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<Topic> listThemes = _db.Topics.ToList();
            return View(listThemes);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Topic obj)
        {
            return View();
        }
    }
}

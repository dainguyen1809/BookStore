using Book_Store.Models;
using Book_Store.Repository.Data;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Areas.Admin.Controllers
{
    public class PublishersController : Controller
    {
        private readonly DataContext _db;
        public PublishersController(DataContext db)
        {
            _db = db; 
        }

        [Area("Admin")]
        public IActionResult Index()
        {
            List<Publisher> listPublishers = _db.Publishers.ToList();
            return View(listPublishers);
        }
    }
}

using Book_Store.Models;
using Book_Store.Repository.Data;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Areas.Admin.Controllers
{
    public class BooksController : Controller
    {
        private readonly DataContext _db;
        public BooksController(DataContext db)
        {
            _db = db;
        }

		[Area("Admin")]
		public IActionResult Index()    
        {
            List<Book> listBooks = _db.Books.ToList();
            return View(listBooks);
        }

        [Area("Admin")]
        public IActionResult Create()
		{
			return View("Create");
		}

		[HttpPost]
		public IActionResult Create(Book obj, IFormFile img)
		{
			if (ModelState.IsValid)
			{
				if (img != null && img.Length > 0)
				{
					var imgPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", img.FileName);
					using (var stream = new FileStream(imgPath, FileMode.Create))
					{
						img.CopyTo(stream);
					}
					obj.UrlImgCover = "/Images/" + img.FileName;
				}
				_db.Books.Add(obj);
				_db.SaveChanges();
			}
			return RedirectToAction("Index");
		}
	}
}

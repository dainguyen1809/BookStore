using Book_Store.Models;
using Book_Store.Repository.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Book_Store.Controllers
{
	public class BooksController : Controller
	{
		private readonly DataContext _db;
        public BooksController(DataContext db)
        {
            _db = db;
        }


        public IActionResult Index()
		{
			List<Book> listBooks = _db.Books.ToList();
			return View(listBooks);
		}

		public IActionResult Details(int id)
		{
			var books = _db.Books.Include(b => b.Publisher).FirstOrDefault(b => b.Id == id);

			if(books == null)
			{
				return NotFound();
			}

			return View(books);
		}

        public IActionResult BookByPublisher(int id)
        {
            var bookByPublisher = _db.Books.Where(b => b.PublisherId == id).ToList();
            return View(bookByPublisher);
        }
    }
}

using Book_Store.Models;
using Book_Store.Models.ViewModels;
using Book_Store.Repository;
using Book_Store.Repository.Data;
using Microsoft.AspNetCore.Mvc;

namespace Book_Store.Controllers
{
	public class CartController : Controller
	{
		private readonly DataContext _db;
        public CartController(DataContext db)
        {
            _db = db;
        }
        public IActionResult Index()
		{
			// Lấy kết quả đưa vào Cart
			List<Cart> cartItems = HttpContext.Session.GetJson<List<Cart>>("Cart") ?? new List<Cart>();

			CartViewModel cartVM = new()
			{
				listCartItems = cartItems,

				// tổng tiền
				Total = cartItems.Sum(x => x.Total)
			};
			return View(cartVM);
		}

		public async Task<IActionResult> AddToCart(int Id)
		{
			Book prod = await _db.Books.FindAsync(Id);
            List<Cart> cart = HttpContext.Session.GetJson<List<Cart>>("Cart") ?? new List<Cart>();
			Cart cartItems = cart.Where(c => c.BookId == Id).FirstOrDefault();

			if (cartItems == null)
			{
				//	Thêm SP
				cart.Add(new Cart(prod));
			} else { cartItems.Quantity += 1; }


			//	Đưa cart lưu vào session
			HttpContext.Session.SetJson("Cart", cart);

			return Redirect(Request.Headers["Referer"].ToString());
		} 

    }
}

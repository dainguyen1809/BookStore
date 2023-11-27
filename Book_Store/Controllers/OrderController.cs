using Book_Store.Models;
using Book_Store.Repository;
using Book_Store.Repository.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Book_Store.Controllers
{
    public class OrderController : Controller
    {
        private readonly DataContext _db;

        public OrderController(DataContext db)
        {
            _db = db;
        }

        public IActionResult Checkout()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail == null)
            {
                return RedirectToAction("SignIn", "Account");
            }
            else
            {
                var orderCode = Guid.NewGuid().ToString();
                var orderItems = new Order();
                orderItems.OrderCode = orderCode;
                //orderItems.DeliveryDate = DateTime.Now;
                orderItems.OrderDate = DateTime.Now;
                orderItems.Checkout = 0;
                orderItems.Status = 1;
                orderItems.UserName = userEmail;

                List<Cart> cartItems = HttpContext.Session.GetJson<List<Cart>>("Cart") ?? new List<Cart>();
                foreach(var items in cartItems)
                {
                    var orderDetail = new OrderDetail(); 
                    orderDetail.OrderCode = orderCode;
                    orderDetail.BookId = items.BookId;
                    orderDetail.Price = items.Price;
                    orderDetail.Quantity = items.Quantity;

                    _db.Add(orderDetail);
                    _db.SaveChanges();
                }
                HttpContext.Session.Remove("Cart");

                //_db.Add(orderItems);
                //_db.SaveChanges();

                return Redirect("/");
            }
        }
    }
}

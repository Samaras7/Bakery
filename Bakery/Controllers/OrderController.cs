using Bakery.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bakery.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository orderRepository;
        private readonly IShoppingCart shoppingCart;

        public OrderController(IOrderRepository orderRepository, IShoppingCart shoppingCart)
        {
            this.orderRepository = orderRepository;
            this.shoppingCart = shoppingCart;
        }
        
        public IActionResult Checkout()
        {
            return View();
        }
    }
}

namespace Bakery.Models
{
    public class OrderRepository : IOrderRepository
    {
        private readonly BakeryDbContext _bakeryDbContext;
        private readonly IShoppingCart _shoppingCart;

        public OrderRepository(BakeryDbContext bakeryDbContext, IShoppingCart shoppingCart)
        {
            _bakeryDbContext = bakeryDbContext;
            _shoppingCart = shoppingCart;
        }

        public void CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;

            List<ShoppingCartItem>? shoppingCartItems = _shoppingCart.ShoppingCartItems;
            order.OrderTotal = _shoppingCart.GetShoppingCartTotal();

            order.OrderDetails = new List<OrderDetail>();

            foreach (ShoppingCartItem? shoppingCartItem in shoppingCartItems)
            {
                var orderDetail = new OrderDetail
                {
                    Amount = shoppingCartItem.Amount,
                    PieId = shoppingCartItem.Pie.PieId,
                    Price = shoppingCartItem.Pie.Price
                };

                order.OrderDetails.Add(orderDetail);
            }

            _bakeryDbContext.Orders.Add(order);

            _bakeryDbContext.SaveChanges();
        }
    }
}

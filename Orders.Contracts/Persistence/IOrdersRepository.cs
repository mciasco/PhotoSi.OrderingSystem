using Orders.Contracts.Domain;

namespace Orders.Contracts.Persistence
{
    public interface IOrdersRepository
    {
        Task<IEnumerable<Order>> GetAllOrders();
        Task<Order> GetOrderById(string orderId);
        Task AddOrder(Order order);
    }
}

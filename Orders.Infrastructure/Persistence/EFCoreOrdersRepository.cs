using Microsoft.EntityFrameworkCore;
using Orders.Contracts.Domain;
using Orders.Contracts.Persistence;

namespace Orders.Infrastructure.Persistence
{
    public class EFCoreOrdersRepository : IOrdersRepository
    {
        private readonly OrdersDbContext _dbContext;

        public EFCoreOrdersRepository(OrdersDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task AddOrder(Order order)
        {
            await _dbContext.Orders.AddAsync(order);
        }

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            return await _dbContext
                .Orders
                .Include(o => o.OrderedProducts)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetAllOrdersByAccountId(string input)
        {
            return await _dbContext.Orders
                .Include(o => o.OrderedProducts)
                .Where(o => o.CustomerAccountId == input).ToArrayAsync();
        }

        public async Task<Order> GetOrderById(string orderId)
        {
            return await _dbContext
                .Orders
                .Include(o => o.OrderedProducts)
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }
    }

}

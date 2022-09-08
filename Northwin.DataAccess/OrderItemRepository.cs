using Northwin.Models;
using Northwind.Repositories;

namespace Northwin.DataAccess
{
    public class OrderItemRepository : Repository<OrderItem>, IOrderItemRepository
    {
        public OrderItemRepository(string connectionString) : base(connectionString)
        {
        }
    }
}

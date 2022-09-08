using Northwin.Models;

namespace Northwind.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        IEnumerable<Order> OrderPagedList(int page, int rows);
    }
}

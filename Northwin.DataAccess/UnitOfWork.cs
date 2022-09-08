using Northwind.Repositories;
using Northwind.UnitOfWork;

namespace Northwin.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(string conn)
        {
            Customer = new CustomerRepository(conn);
            User = new UserRepository(conn);
            Supplier =  new SuppplierRepository(conn);
            Order = new OrderRepository(conn);
            Product = new ProductRepository(conn);
            OrderItem = new OrderItemRepository(conn);
        }

        public ICustomerRepository Customer { get; private set; }

        public IUserRepository User { get; private set; }

        public ISupplierRepository Supplier { get; private set; }

        public IOrderRepository Order { get; private set; }

        public IProductRepository Product { get; private set; }

        public IOrderItemRepository OrderItem { get; private set; }
    }
}

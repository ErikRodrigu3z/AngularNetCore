using Northwind.Repositories;
using Northwind.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwin.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(string conn)
        {
            Customer = new CustomerRepository(conn);
            User = new UserRepository(conn);
            Supplier =  new SuppplierRepository(conn);
        }

        public ICustomerRepository Customer { get; private set; }

        public IUserRepository User { get; private set; }

        public ISupplierRepository Supplier { get; private set; }
    }
}

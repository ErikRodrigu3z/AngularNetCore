using Northwin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> ProductPagedList(int page, int rows);
    }
}

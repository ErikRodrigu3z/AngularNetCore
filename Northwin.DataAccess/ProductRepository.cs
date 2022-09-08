using Dapper;
using Northwin.Models;
using Northwind.Repositories;
using System.Data.SqlClient;

namespace Northwin.DataAccess
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(string connectionString) : base(connectionString)
        {
        }

        public IEnumerable<Product> ProductPagedList(int page, int rows)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@page", page);
            parameters.Add("@rows", rows);

            using (var conn = new SqlConnection(_connectionString))
            {
                return conn.Query<Product>("dbo.ProductPagedList",
                    parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }
    }
}

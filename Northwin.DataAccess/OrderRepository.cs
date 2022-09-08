using Dapper;
using Northwin.Models;
using Northwind.Repositories;
using System.Data.SqlClient;

namespace Northwin.DataAccess
{
    internal class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(string connectionString) : base(connectionString)
        {
        }

        public IEnumerable<Order> OrderPagedList(int page, int rows)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@page", page);
            parameters.Add("@rows", rows);

            using (var conn = new SqlConnection(_connectionString))
            {
                return conn.Query<Order>("dbo.OrderPagedList",
                    parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }
    }
}

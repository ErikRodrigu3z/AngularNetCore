using Dapper;
using Northwin.Models;
using Northwind.Repositories;
using System.Data.SqlClient;

namespace Northwin.DataAccess
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(string connectionString) : base(connectionString)
        {
            
        }

        public IEnumerable<Customer> CustomerPagedList(int page, int rows)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@page", page);
            parameters.Add("@rows", rows);

            using (var conn = new SqlConnection(_connectionString))
            {
                return conn.Query<Customer>("dbo.CustomerPagedList",
                    parameters, commandType: System.Data.CommandType.StoredProcedure);
            }

        }
    }
}

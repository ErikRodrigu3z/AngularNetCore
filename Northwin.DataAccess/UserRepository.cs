using Dapper;
using Northwin.Models;
using Northwind.Repositories;
using System.Data.SqlClient;

namespace Northwin.DataAccess
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(string connectionString) : base(connectionString)
        {
        }

        public User ValidateUser(string email, string password)
        {
            var paramaters = new DynamicParameters();
            paramaters.Add("@email", email);
            paramaters.Add("@password", password);

            using (var conn = new SqlConnection(_connectionString))
            {
                return conn.QueryFirstOrDefault<User>(
                        "dbo.validateUser", paramaters,
                        commandType: System.Data.CommandType.StoredProcedure);
            }
        }
    }
}

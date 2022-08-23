using Dapper;
using Northwin.Models;
using Northwind.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwin.DataAccess
{
    public class SuppplierRepository : Repository<Supplier>, ISupplierRepository
    {
        public SuppplierRepository(string connectionString) : base(connectionString)
        {
        }

        public IEnumerable<Supplier> SupplierPagedList(int page, int rows)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@page",page);
            parameters.Add("@rows",rows);

            using (var conn = new SqlConnection(_connectionString))
            {
                return conn.Query<Supplier>("dbo.SupplierPagedList", parameters,
                        commandType: System.Data.CommandType.StoredProcedure);
            }

        }
    }
}

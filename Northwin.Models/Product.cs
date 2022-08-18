using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwin.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = null!;
        public int SupplierId { get; set; }
        public decimal? UnitPrice { get; set; } = null!;
        public string Package { get; set; } = null!;
        public bool IsDiscontinued { get; set; }
    }
}

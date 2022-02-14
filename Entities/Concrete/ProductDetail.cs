using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class ProductDetail
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Description { get; set; }
        public int QuantityPerUnit { get; set; }
        public double UnitPrice { get; set; }
        public int UnitInStock { get; set; }
        public int UnitOnOrder { get; set; }
    }
}

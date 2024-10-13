using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Order_Aggregate
{
    public class OrderItem : BaseEntity
    {
        public ProductItemOrderd Product { get; set; }
        public string Quantity { get; set; }
        public decimal Price { get; set; }
    }
}

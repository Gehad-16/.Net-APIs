using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class ProductWithBrandAndTypeSpecifications: BaseSpecifications<Product>
    {
        public ProductWithBrandAndTypeSpecifications():base()
        {
            Include.Add(p => p.productBrand);
            Include.Add(p => p.productType);
        }
        public ProductWithBrandAndTypeSpecifications(int id) : base(p=>p.Id == id)
        {
            Include.Add(p => p.productBrand);
            Include.Add(p => p.productType);
        }
    }
}

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
        public ProductWithBrandAndTypeSpecifications(ProductSpecParams productSpecParams)
            :base(p=>(string.IsNullOrEmpty(productSpecParams.Search)||p.Name.ToLower().Contains(productSpecParams.Search))&&
                     (!productSpecParams.BrandID.HasValue || p.ProductBrandId== productSpecParams.BrandID)&&
                     (!productSpecParams.TypeID.HasValue || p.ProductTypeId== productSpecParams.TypeID))
        {
            Include.Add(p => p.productBrand);
            Include.Add(p => p.productType);
            if(! string.IsNullOrEmpty(productSpecParams.Sort))
            {
                switch(productSpecParams.Sort)
                {
                    case "Price":
                        AddOrderBy(p => p.Price);
                        break;
                    case "PriceDes":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;

                }
            }
            ApplyPagination(productSpecParams.PageSize*(productSpecParams.PageIndex-1),productSpecParams.PageSize);
        }
        public ProductWithBrandAndTypeSpecifications(int id) : base(p=>p.Id == id)
        {
            Include.Add(p => p.productBrand);
            Include.Add(p => p.productType);
        }
    }
}

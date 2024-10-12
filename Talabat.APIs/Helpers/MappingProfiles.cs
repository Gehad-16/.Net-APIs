using AutoMapper;
using Talabat.APIs.DTOs;
using Talabat.Core.Entities;

namespace Talabat.APIs.Helpers
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductTOReturnDTO>().ForMember(d => d.productType, o => o.MapFrom(o => o.productType.Name))
                                                    .ForMember(d => d.productBrand, o => o.MapFrom(o => o.productBrand.Name))
                                                    /*.ForMember(d => d.PictureUrl, o => o.MapFrom<ProductPictureURLResolver>())*/;
        }
    }
}

using AutoMapper;
using Matger.Core.Entities;
using Matger.Core.Entities.Order_Aggregate;
using Matger.DTOs;

namespace Matger.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(PDto => PDto.ProductTypes, o => o.MapFrom(p => p.ProductTypes.Name))
                .ForMember(PDto => PDto.ProductBrand, o => o.MapFrom(p => p.ProductBrand.Name))
                .ForMember(PDto => PDto.PictureUrl, o => o.MapFrom<ProductPactureUrlResolver>());


             CreateMap<AddressDto,Address>();


            
        }
    }
}

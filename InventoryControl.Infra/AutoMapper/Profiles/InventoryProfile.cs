using AutoMapper;
using InventoryControl.Domain.Dtos.Inventory;
using InventoryControl.Domain.Dtos.Requests.Inventory;
using InventoryControl.Domain.Entities;

namespace InventoryControl.Infra.AutoMapper.Profiles;

public class InventoryProfile : Profile
{
    public InventoryProfile()
    {
        CreateMap<PostProductRequest, Product>();

        CreateMap<ProductDto, Product>().ReverseMap()
            .ForMember(src => src.Inventory, dest => dest.MapFrom(x => x.Inventory))
            .ForMember(src => src.Supplier, dest => dest.MapFrom(x => x.Supplier));

        CreateMap<SupplierDto, Supplier>().ReverseMap();

        CreateMap<InventoryDto, Inventory>().ReverseMap();
    }
}
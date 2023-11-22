using AutoMapper;
using InventoryControl.Domain.Dtos.Requests.Supplier;
using InventoryControl.Domain.Entities;

namespace InventoryControl.Infra.AutoMapper.Profiles;

public class SupplierProfile : Profile
{
    public SupplierProfile()
    {
        CreateMap<PostSupplierRequest, Supplier>();

        CreateMap<SupplierPatchDocument, Supplier>().ReverseMap();
    }
}
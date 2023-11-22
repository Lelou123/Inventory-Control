using AutoMapper;
using InventoryControl.Domain.Dtos.Inventory;
using InventoryControl.Domain.Dtos.Requests.Purchase;
using InventoryControl.Domain.Dtos.Requests.Transaction;
using InventoryControl.Domain.Dtos.Transaction;
using InventoryControl.Domain.Dtos.Transactions;
using InventoryControl.Domain.Entities;
using InventoryControl.Domain.Enums;

namespace InventoryControl.Infra.AutoMapper.Profiles;

public class TransactionProfile : Profile
{
    public TransactionProfile()
    {
        CreateMap<PostSaleRequest, Transaction>()
            .ForMember(dest => dest.Type,
                src => src.MapFrom(x => EnTransactionType.Sale));


        CreateMap<Transaction, TransactionDto>().ReverseMap();
        CreateMap<Transaction, TransactionUpdateDto>().ReverseMap();

        CreateMap<Transaction, SaleDto>()
            .ForMember(dest => dest.Product, src =>
                src.MapFrom(x => x.Inventory.Product));


        CreateMap<PostPurchaseRequest, Transaction>()
            .ForMember(dest => dest.Type,
                src => src.MapFrom(x => EnTransactionType.Purchase));
        
        
        CreateMap<Transaction, PurchaseDto>()
            .ForMember(dest => dest.Product, src =>
                src.MapFrom(x => x.Inventory.Product));
        
    }
}
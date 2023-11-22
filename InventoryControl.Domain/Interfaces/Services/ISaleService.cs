using InventoryControl.Domain.Dtos.Requests.Transaction;
using InventoryControl.Domain.Dtos.Responses.Sale;
using InventoryControl.Domain.Dtos.Transaction;
using Microsoft.AspNetCore.JsonPatch;

namespace InventoryControl.Domain.Interfaces.Services;

public interface ISaleService
{
    Task<PostSaleResponse> PostSale(PostSaleRequest request);

    Task<GetSalesResponse> GetSales();

    Task<GetSaleDetailsResponse> GetSaleDetails(Guid id);

    Task<UpdateSalesResponse> UpdateSale(UpdateSaleRequest request);

    Task<DeleteSaleResponse> DeleteSale(Guid id);
}
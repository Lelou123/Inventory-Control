using InventoryControl.Domain.Dtos.Requests.Purchase;
using InventoryControl.Domain.Dtos.Responses.Purchase;

namespace InventoryControl.Domain.Interfaces.Services;

public interface IPurchaseService
{
    Task<PostPurchaseResponse> PostPurchase(PostPurchaseRequest request);

    Task<GetPurchasesResponse> GetPurchases();

    Task<GetPurchaseDetailsResponse> GetPurchaseDetails(Guid id);

    Task<DeletePurchaseResponse> DeletePurchase(Guid id);

    Task<UpdatePurchaseResponse> UpdatePurchase(UpdatePurchaseRequest request);
}
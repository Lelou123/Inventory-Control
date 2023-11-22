using InventoryControl.Domain.Dtos.Inventory;
using InventoryControl.Domain.Interfaces;

namespace InventoryControl.Domain.Dtos.Responses.Purchase;

public class GetPurchaseDetailsResponse : IResponseBase<PurchaseDto>
{
    public PurchaseDto? Data { get; set; }
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
}
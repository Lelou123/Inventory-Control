using InventoryControl.Domain.Interfaces;

namespace InventoryControl.Domain.Dtos.Responses.Purchase;

public class UpdatePurchaseResponse : IResponseBase<Guid?>
{
    public Guid? Data { get; set; }
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
}
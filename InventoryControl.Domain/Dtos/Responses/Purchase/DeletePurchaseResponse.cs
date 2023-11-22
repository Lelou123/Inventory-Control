using InventoryControl.Domain.Interfaces;

namespace InventoryControl.Domain.Dtos.Responses.Purchase;

public class DeletePurchaseResponse : IResponseBase<string?>
{
    public string? Data { get; set; }
    
    public bool IsSuccess { get; set; }
    
    public string? Message { get; set; }
}
using InventoryControl.Domain.Interfaces;

namespace InventoryControl.Domain.Dtos.Responses.Inventory;

public class PostProductResponse : IResponseBase<Guid?>
{
    public Guid? Data { get; set; }

    public bool IsSuccess { get; set; }

    public string? Message { get; set; }
}
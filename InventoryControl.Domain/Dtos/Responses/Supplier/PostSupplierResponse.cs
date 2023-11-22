using InventoryControl.Domain.Interfaces;

namespace InventoryControl.Domain.Dtos.Responses.Supplier;

public class PostSupplierResponse : IResponseBase<Guid?>
{
    public Guid? Data { get; set; }
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
}
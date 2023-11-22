using InventoryControl.Domain.Interfaces;

namespace InventoryControl.Domain.Dtos.Responses.Supplier;

public class DeleteSupplierResponse : IResponseBase<string?>
{
    public string? Data { get; set; }
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
}
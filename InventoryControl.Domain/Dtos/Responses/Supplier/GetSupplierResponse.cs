using InventoryControl.Domain.Dtos.Inventory;
using InventoryControl.Domain.Interfaces;

namespace InventoryControl.Domain.Dtos.Responses.Supplier;

public class GetSupplierResponse : IResponseBase<List<SupplierDto>>
{
    public List<SupplierDto>? Data { get; set; }
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
}
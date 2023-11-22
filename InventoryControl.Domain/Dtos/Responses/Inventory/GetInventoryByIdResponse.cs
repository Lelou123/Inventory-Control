using InventoryControl.Domain.Dtos.Inventory;
using InventoryControl.Domain.Interfaces;

namespace InventoryControl.Domain.Dtos.Responses.Inventory;

public class GetInventoryByIdResponse : IResponseBase<ProductDto?>
{
    public ProductDto? Data { get; set; }
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
}
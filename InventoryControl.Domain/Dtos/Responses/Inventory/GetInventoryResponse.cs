using InventoryControl.Domain.Dtos.Inventory;
using InventoryControl.Domain.Enums;
using InventoryControl.Domain.Interfaces;

namespace InventoryControl.Domain.Dtos.Responses.Inventory;

public class GetInventoryResponse : IResponseBase<List<ProductDto>>
{
    public List<ProductDto>? Data { get; set; }
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
}
using InventoryControl.Domain.Dtos.Inventory;
using InventoryControl.Domain.Interfaces;

namespace InventoryControl.Domain.Dtos.Responses.Sale;

public class GetSaleDetailsResponse : IResponseBase<SaleDto>
{
    public SaleDto? Data { get; set; }
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
}
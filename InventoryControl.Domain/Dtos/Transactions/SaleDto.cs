using InventoryControl.Domain.Dtos.Transaction;

namespace InventoryControl.Domain.Dtos.Inventory;

public class SaleDto : TransactionDto
{
    public ProductDto? Product { get; set; }
}
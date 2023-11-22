using InventoryControl.Domain.Dtos.Transaction;

namespace InventoryControl.Domain.Dtos.Inventory;

public class PurchaseDto : TransactionDto
{
    public ProductDto? Product { get; set; }
}
using InventoryControl.Domain.Dtos.Transactions;

namespace InventoryControl.Domain.Dtos.Requests.Purchase;

public class UpdatePurchaseRequest
{
    public Guid Id { get; set; }
    
    public TransactionUpdateDto TransactionPatch { get; set; }
}
using InventoryControl.Domain.Enums;

namespace InventoryControl.Domain.Dtos.Transaction;

public class TransactionDto
{
    public Guid Id { get; set; }

    public int Quantity { get; set; }

    public double Price { get; set; }

    public EnTransactionType Type { get; set; }

    public Guid InventoryId { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime? UpdatedAt { get; set; }
}
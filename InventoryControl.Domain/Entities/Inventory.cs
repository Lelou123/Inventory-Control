using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryControl.Domain.Entities;

public class Inventory : BaseEntity
{
    public int Quantity { get; set; }

    public int MinQuantity { get; set; }

    public double TotalValue { get; set; } = 0;

    public Guid ProductId { get; set; }

    [ForeignKey(nameof(ProductId))] public virtual Product? Product { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; }
}
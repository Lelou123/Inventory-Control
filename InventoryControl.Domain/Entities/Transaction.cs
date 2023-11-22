using System.ComponentModel.DataAnnotations.Schema;
using InventoryControl.Domain.Enums;

namespace InventoryControl.Domain.Entities;

public class Transaction : BaseEntity
{
    public int Quantity { get; set; }

    public double Price { get; set; }

    public EnTransactionType Type { get; set; }

    public Guid InventoryId { get; set; }

    [ForeignKey(nameof(InventoryId))] public virtual Inventory Inventory { get; set; }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using InventoryControl.Domain.Enums;

namespace InventoryControl.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Code { get; set; } = string.Empty;

    public EnProductCategory ProductCategory { get; set; }

    public double Weight { get; set; }

    public virtual Inventory? Inventory { get; set; }

    public Guid SupplierId { get; set; }

    [ForeignKey(nameof(SupplierId))] public virtual Supplier? Supplier { get; set; }
}
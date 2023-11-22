using InventoryControl.Domain.Interfaces;

namespace InventoryControl.Domain.Entities;

public class Supplier : BaseEntity
{
    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<Product>? Products { get; set; }
}
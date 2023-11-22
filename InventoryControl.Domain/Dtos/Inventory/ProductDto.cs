using InventoryControl.Domain.Enums;

namespace InventoryControl.Domain.Dtos.Inventory;

public class ProductDto
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Code { get; set; }

    public EnProductCategory ProductCategory { get; set; }

    public double Weight { get; set; }

    public InventoryDto? Inventory { get; set; }

    public SupplierDto? Supplier { get; set; }
}
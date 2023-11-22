using InventoryControl.Domain.Dtos.Inventory;
using InventoryControl.Domain.Enums;

namespace InventoryControl.Domain.Dtos.Requests.Inventory;

public class PostProductRequest
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    public double Weight { get; set; }

    public EnProductCategory ProductCategory { get; set; }
    public double Price { get; set; }

    public InventoryDto? Inventory { get; set; }

    public Guid SupplierId { get; set; }
}
namespace InventoryControl.Domain.Dtos.Inventory;

public class InventoryDto
{
    public Guid Id { get; set; }

    public int Quantity { get; set; }

    public int MinQuantity { get; set; }
}
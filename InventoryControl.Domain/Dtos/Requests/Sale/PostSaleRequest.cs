using InventoryControl.Domain.Enums;

namespace InventoryControl.Domain.Dtos.Requests.Transaction;

public class PostSaleRequest
{
    public int Quantity { get; set; }

    public double Price { get; set; }

    public Guid InventoryId { get; set; }
}
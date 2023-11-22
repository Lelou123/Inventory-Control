namespace InventoryControl.Domain.Dtos.Requests.Purchase;

public class PostPurchaseRequest
{
    public int Quantity { get; set; }

    public double Price { get; set; }

    public Guid InventoryId { get; set; }
}
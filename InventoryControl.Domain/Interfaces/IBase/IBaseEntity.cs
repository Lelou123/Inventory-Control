namespace InventoryControl.Domain.Interfaces;

public interface IBaseEntity
{
    public Guid Id { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }
}
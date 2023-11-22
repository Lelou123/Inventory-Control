using InventoryControl.Domain.Interfaces;

namespace InventoryControl.Domain.Entities;

public abstract class BaseEntity : IBaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public DateTime? UpdatedAt { get; set; } = null;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public bool IsActive { get; set; } = true;

    public bool IsDeleted { get; set; }
}
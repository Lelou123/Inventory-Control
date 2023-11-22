using InventoryControl.Domain.Dtos.Responses;

namespace InventoryControl.Domain.Interfaces.IBase;

public interface IEntityValidator<in T>
{
    Task<ValidatorResponse> Validate(T entity);
}
using InventoryControl.Domain.Dtos.Responses;
using InventoryControl.Domain.Entities;
using InventoryControl.Domain.Interfaces.Validators;

namespace InventoryControl.Infra.Validators;

public class InventoryValidator : IInventoryValidator
{
    public async Task<ValidatorResponse> Validate(Inventory entity)
    {
        var validationResult = await new FluentInventoryValidator().ValidateAsync(entity);

        var errorsMessages = validationResult.Errors.Select(x => x.ErrorMessage).ToList();

        return new ValidatorResponse()
        {
            ErrorsMessages = errorsMessages,
            IsValid = validationResult.IsValid
        };
    }
}
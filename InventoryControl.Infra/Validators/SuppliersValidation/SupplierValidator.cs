using InventoryControl.Domain.Dtos.Responses;
using InventoryControl.Domain.Entities;
using InventoryControl.Domain.Interfaces.Validators;

namespace InventoryControl.Infra.Validators.SuppliersValidation;

public class SupplierValidator : ISupplierValidator
{
    public async Task<ValidatorResponse> Validate(Supplier entity)
    {
        var validationResult = await new FluentSuppliersValidator().ValidateAsync(entity);

        var errorsMessages = validationResult.Errors.Select(x => x.ErrorMessage).ToList();

        return new ValidatorResponse()
        {
            ErrorsMessages = errorsMessages,
            IsValid = validationResult.IsValid
        };
    }
}
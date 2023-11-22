using InventoryControl.Domain.Dtos.Responses;
using InventoryControl.Domain.Entities;
using InventoryControl.Domain.Interfaces.Validators;

namespace InventoryControl.Infra.Validators.TransactionValidator;

public class TransactionValidator : ITransactionValidator
{
    public async Task<ValidatorResponse> Validate(Transaction entity)
    {
        var validationResult =
            await new FluentTransactionValidator().ValidateAsync(entity);

        var errorsMessages = validationResult.Errors.Select(x => x.ErrorMessage).ToList();

        return new ValidatorResponse()
        {
            ErrorsMessages = errorsMessages,
            IsValid = validationResult.IsValid
        };
    }
}
using FluentValidation;
using FluentValidation.Results;
using InventoryControl.Domain.Dtos.Responses;
using InventoryControl.Domain.Entities;
using InventoryControl.Domain.Interfaces.Validators;

namespace InventoryControl.Infra.Validators;

public class ProductValidator : IProductValidator
{
    public async Task<ValidatorResponse> Validate(Product entity)
    {
        var validationResult = await new FluentProductValidator().ValidateAsync(entity);

        var errorsMessages = validationResult.Errors.Select(x => x.ErrorMessage).ToList();

        return new ValidatorResponse()
        {
            ErrorsMessages = errorsMessages,
            IsValid = validationResult.IsValid
        };
    }
}
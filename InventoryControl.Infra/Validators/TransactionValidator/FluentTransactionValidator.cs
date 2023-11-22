using FluentValidation;
using InventoryControl.Domain.Entities;

namespace InventoryControl.Infra.Validators.TransactionValidator;

public class FluentTransactionValidator : AbstractValidator<Transaction>
{
    public FluentTransactionValidator()
    {
        RuleFor(x => x.Type).IsInEnum();
        RuleFor(x => x.Price).GreaterThan(0);
        RuleFor(x => x.Quantity).GreaterThan(0);
    }
}
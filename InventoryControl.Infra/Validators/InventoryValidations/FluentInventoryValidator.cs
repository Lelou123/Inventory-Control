using FluentValidation;
using InventoryControl.Domain.Entities;

namespace InventoryControl.Infra.Validators;

public class FluentInventoryValidator : AbstractValidator<Inventory>
{
    public FluentInventoryValidator()
    {
        RuleFor(x => x.Quantity).GreaterThanOrEqualTo(0);
        RuleFor(x => x.MinQuantity).GreaterThan(0);
        RuleFor(x => x.TotalValue).GreaterThan(0);
    }
}
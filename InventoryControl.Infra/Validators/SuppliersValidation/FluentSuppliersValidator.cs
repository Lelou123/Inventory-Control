using FluentValidation;
using InventoryControl.Domain.Entities;

namespace InventoryControl.Infra.Validators.SuppliersValidation;

public class FluentSuppliersValidator : AbstractValidator<Supplier>
{
    public FluentSuppliersValidator()
    {
        RuleFor(x => x.Name).MinimumLength(2).MaximumLength(100);
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Address).NotEmpty().MinimumLength(2).MaximumLength(100);
        RuleFor(x => x.Phone).Matches(@"^\+55(\s?\(?\d{2}\)?\s?)?(\d{4,5}\-?\d{4})$");
    }
}
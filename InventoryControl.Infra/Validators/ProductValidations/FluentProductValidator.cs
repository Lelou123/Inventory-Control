using FluentValidation;
using InventoryControl.Domain.Entities;

namespace InventoryControl.Infra.Validators;

public class FluentProductValidator : AbstractValidator<Product>
{
    public FluentProductValidator()
    {
        RuleFor(x => x.ProductCategory).IsInEnum().NotNull();
        RuleFor(x => x.Name).MinimumLength(2).MaximumLength(100).NotNull().NotEmpty();
        RuleFor(x => x.Description).MinimumLength(2).MaximumLength(100).NotNull()
            .NotEmpty();
        RuleFor(x => x.Weight).GreaterThan(1).NotNull();

        //Inventory
        RuleFor(x => x.Inventory.Quantity).GreaterThan(0);
        RuleFor(x => x.Inventory.MinQuantity).GreaterThan(0);
        RuleFor(x => x.Inventory.TotalValue).GreaterThan(0);

        //Supplier
        RuleFor(x => x.Supplier.Name).MinimumLength(2).MaximumLength(100);
        RuleFor(x => x.Supplier.Email).EmailAddress();
        RuleFor(x => x.Supplier.Address).NotEmpty().MinimumLength(2).MaximumLength(100);
        RuleFor(x => x.Supplier.Phone)
            .Matches(@"^\+55(\s?\(?\d{2}\)?\s?)?(\d{4,5}\-?\d{4})$");
    }
}
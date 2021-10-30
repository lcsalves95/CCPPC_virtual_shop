using FluentValidation;

namespace CCPPC.VirtualShop.Application.Models.Validations
{
    public class ItemOrderViewModelValidator : AbstractValidator<ItemOrderViewModel>
    {
        public ItemOrderViewModelValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThanOrEqualTo(1).WithMessage("The [ProductId] field must be valid");
            RuleFor(x => x.ProductName)
                .NotEmpty().WithMessage("The [ProductName] field is required")
                .NotNull().WithMessage("The [ProductName] field is required")
                .MaximumLength(250).WithMessage("The [ProductName] field must have a maximum of 250 characters");
            RuleFor(x => x.Quantity)
                .GreaterThanOrEqualTo(1).WithMessage("The [Quantity] field must be valid");
            RuleFor(x => x.UnitaryValue)
                .GreaterThan(0).WithMessage("The [UnitaryValue] field must be valid");
        }
    }
}

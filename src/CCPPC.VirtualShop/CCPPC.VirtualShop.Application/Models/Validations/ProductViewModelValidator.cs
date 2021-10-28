using FluentValidation;

namespace CCPPC.VirtualShop.Application.Models.Validations
{
    public class ProductViewModelValidator : AbstractValidator<ProductViewModel>
    {
        public ProductViewModelValidator()
        {
            RuleFor(n => n.Name)
                .NotEmpty().NotNull().WithMessage("The [Name] field is required")
                .MaximumLength(250).WithMessage("The [Name] field must have a maximum of 250 characters");
            RuleFor(d => d.Description)
                .NotEmpty().NotNull().WithMessage("The [Description] field is required")
                .MaximumLength(250).WithMessage("The [Description] field must have a maximum of 500 characters");
            RuleFor(v => v.Value)
                .GreaterThan(0).WithMessage("The [Value] field must be valid");
            RuleFor(q => q.Quantity)
                .GreaterThan(0).WithMessage("The [Quantity] field must be valid");
        }
    }
}

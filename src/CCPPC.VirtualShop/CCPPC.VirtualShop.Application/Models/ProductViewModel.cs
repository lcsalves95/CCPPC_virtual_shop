using CCPPC.VirtualShop.Application.Models.Validations;
using FluentValidation.Results;

namespace CCPPC.VirtualShop.Application.Models
{
    public class ProductViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public double Value { get; set; }

        public ValidationResult Validate()
        {
            return new ProductViewModelValidator().Validate(this);
        }
    }
}

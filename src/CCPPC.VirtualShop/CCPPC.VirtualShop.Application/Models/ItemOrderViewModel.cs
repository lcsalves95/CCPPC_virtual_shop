using CCPPC.VirtualShop.Application.Models.Validations;
using FluentValidation.Results;

namespace CCPPC.VirtualShop.Application.Models
{
    public class ItemOrderViewModel
    {
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double UnitaryValue { get; set; }

        public ValidationResult Validate()
        {
            return new ItemOrderViewModelValidator().Validate(this);
        }
    }
}

using System;

namespace CCPPC.VirtualShop.Domain.Entities
{
    public class Product : BaseEntity
    {
        public Product(string name, string description, int stockQuantity, double value)
        {
            Name = name;
            Description = description;
            StockQuantity = stockQuantity;
            Value = value;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public int StockQuantity { get; private set; }
        public double Value { get; private set; }

        public void UpdateName(string name)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
            Name = name;
        }
    }
}

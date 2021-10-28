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
            CreatedAt = DateTime.Now;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public int StockQuantity { get; private set; }
        public double Value { get; private set; }

        public void AddStockQuantity(int quantity) => StockQuantity += quantity;
        public void UpdateName(string name) => Name = name;
        public void UpdateDescription(string description) => Description = description;
        public void UpdateValue(double value) => Value = value;
        public void UpdateStockQuantity(int quantity) => StockQuantity = quantity;
    }
}

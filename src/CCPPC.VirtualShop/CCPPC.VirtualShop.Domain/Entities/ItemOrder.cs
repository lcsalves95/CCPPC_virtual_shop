namespace CCPPC.VirtualShop.Domain.Entities
{
    public class ItemOrder : BaseEntity
    {
        public ItemOrder(long orderId, long productId, string productName, int quantity, double unitaryValue)
        {
            OrderId = orderId;
            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            UnitaryValue = unitaryValue;
        }

        public long ProductId { get; private set; }
        public string ProductName { get; private set; }
        public int Quantity { get; private set; }
        public double UnitaryValue { get; private set; }

        public long OrderId { get; private set; }
        public Order Order { get; private set; }

        public void AddQuantity(int quantity) => Quantity += quantity;
        public void UpdateQuantity(int quantity) => Quantity = quantity;
        public double CalculateValue() => Quantity * UnitaryValue;
    }
}

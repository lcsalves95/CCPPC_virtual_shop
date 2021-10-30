using CCPPC.VirtualShop.Domain.Enums;
using System.Collections.Generic;
using System.Linq;

namespace CCPPC.VirtualShop.Domain.Entities
{
    public class Order : BaseEntity
    {
        private readonly List<ItemOrder> _intemOrders;

        public long? UserId { get; private set; }
        public double Amount { get; private set; }
        public OrderStatus Status { get; private set; }
        public IReadOnlyCollection<ItemOrder> ItemOrders => _intemOrders;

        public Order (long userId)
        {
            UserId = userId;
            Status = OrderStatus.Initiate;
            _intemOrders = new List<ItemOrder>();
        }
        public Order()
        {
            UserId = null;
            Status = OrderStatus.Draft;
            _intemOrders = new List<ItemOrder>();
        }

        public bool ItemOrderExists(long productId) => ItemOrders.Any(x => x.ProductId == productId);

        public void AddItemOrder(ItemOrder item)
        {
            if (ItemOrderExists(item.ProductId))
            {
                var itemExists = _intemOrders.First(x => x.ProductId == item.ProductId);
                itemExists.AddQuantity(item.Quantity);
                item = itemExists;
                _intemOrders.Remove(itemExists);
            }
            _intemOrders.Add(item);
            UpdateOrderAmount();
        }

        public void UpdateOrderAmount() => Amount = ItemOrders.Sum(i => i.CalculateValue());
    }
}

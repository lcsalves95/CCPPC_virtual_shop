using System;

namespace CCPPC.VirtualShop.Domain.Entities
{
    public class BaseEntity
    {
        public long Id { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

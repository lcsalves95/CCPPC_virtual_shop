using CCPPC.VirtualShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CCPPC.VirtualShop.Data.Mappings
{
    public class ItemOrderMapping : IEntityTypeConfiguration<ItemOrder>
    {
        public void Configure(EntityTypeBuilder<ItemOrder> builder)
        {
            builder.Property(o => o.OrderId).IsRequired();
            builder.Property(p => p.ProductId).IsRequired();
            builder.Property(pn => pn.ProductName).IsRequired().HasMaxLength(250);
            builder.Property(q => q.Quantity).IsRequired();
            builder.Property(v => v.UnitaryValue).IsRequired();
        }
    }
}

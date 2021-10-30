using CCPPC.VirtualShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CCPPC.VirtualShop.Data.Mappings
{
    public class OrderMapping : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Id).UseIdentityColumn();
            builder.Property(u => u.UserId).IsRequired();
            builder.Property(a => a.Amount).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(o => o.Status).IsRequired();
            builder.Property(c => c.CreatedAt).HasDefaultValueSql("GETDATE()");

            builder.HasMany(o => o.ItemOrders)
                .WithOne(x => x.Order)
                .HasForeignKey(x => x.OrderId);
        }
    }
}

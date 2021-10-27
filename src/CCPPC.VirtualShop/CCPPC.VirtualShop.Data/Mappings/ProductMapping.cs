using CCPPC.VirtualShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CCPPC.VirtualShop.Data.Mappings
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(i => i.Id).UseIdentityColumn(1, 1);
            builder.Property(n => n.Name).IsRequired().HasMaxLength(250);
            builder.Property(d => d.Description).IsRequired().HasMaxLength(500);
            builder.Property(q => q.StockQuantity).IsRequired();
            builder.Property(v => v.Value).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(a => a.Active).IsRequired().HasDefaultValue(true);
            builder.Property(c => c.CreatedAt).ValueGeneratedOnAdd();
        }
    }
}

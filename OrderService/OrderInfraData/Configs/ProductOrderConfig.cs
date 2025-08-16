using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderBusiness.Entities;

namespace OrderInfraData.Configs;

public class ProductOrderConfig : IEntityTypeConfiguration<ProductOrder>
{
    public void Configure(EntityTypeBuilder<ProductOrder> builder)
    {
        builder.HasKey(po => po.Id);

        builder
            .HasOne<Order>(po => po.Order)
            .WithMany(o => o.OrderProducts)
            .HasForeignKey(po => po.OrderId);
                
       builder 
            .Property(po => po.Name)
            .IsRequired()
            .HasMaxLength(256);
        
        builder
            .Property(po => po.ProductId)
            .IsRequired();
        
        builder
            .Property(po => po.Price)
            .IsRequired()
            .HasPrecision(18, 2);
        
        builder.Property(po => po.Quantity)
            .IsRequired();
    }
}
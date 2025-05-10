using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SalesOrderManagement.API.Infra.Configurations._bases;
using SalesOrderManagement.Domain.Entities;
using SalesOrderManagement.Domain.Entities._bases;

namespace SalesOrderManagement.API.Infra.Configurations
{
    public class ProductConfiguration : BaseEntityConfiguration<Product>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            TableName = "Products";
            base.Configure(builder);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(Const.NAME_MAX_LENGTH);

            builder.Property(p => p.Description)
                .HasMaxLength(Const.DESCRIPTION_MAX_LENGTH);

            builder.Property(p => p.Price)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(p => p.Quantity)
                .IsRequired();

            builder.Property(p => p.Category)
                .HasMaxLength(Const.CATEGORY_MAX_LENGTH);

            builder.Property(p => p.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            builder.HasMany(p => p.OrderItems)
                .WithOne(oi => oi.Product)
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
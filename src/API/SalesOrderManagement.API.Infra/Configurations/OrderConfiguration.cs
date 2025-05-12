using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SalesOrderManagement.API.Infra.Configurations._bases;
using SalesOrderManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesOrderManagement.Domain.Entities._bases;

namespace SalesOrderManagement.API.Infra.Configurations
{
    public class OrderConfiguration : BaseEntityConfiguration<Order>
    {
        public override void Configure(EntityTypeBuilder<Order> builder)
        {
            TableName = "Orders"; 
            base.Configure(builder);


            builder.Property(o => o.OrderNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(o => o.OrderDate)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(o => o.TotalAmount)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(o => o.ShippingAddress)
                .HasMaxLength(Const.ADDRESS_MAX_LENGTH);

            builder.Property(o => o.BillingAddress)
                .HasMaxLength(Const.ADDRESS_MAX_LENGTH);

            builder.Property(o => o.PaymentMethod)
                .IsRequired();

            builder.Property(o => o.Status)
                .IsRequired()
                .HasConversion<string>(); 

            builder.HasOne(o => o.CreateByUser)
                .WithMany(u => u.CreateByUser)
                .HasForeignKey(o => o.CreateByUserUuid)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.ActionedByUser)
                .WithMany(u => u.ActionedOrders) 
                .HasForeignKey(o => o.ActionedByUserUuid)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            builder.Property(o => o.ActionedAt)
                .IsRequired(false);

            builder.HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
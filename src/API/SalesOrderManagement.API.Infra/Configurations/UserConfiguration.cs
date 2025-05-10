using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SalesOrderManagement.API.Infra.Configurations._bases;
using SalesOrderManagement.Domain.Entities;
using SalesOrderManagement.Domain.Entities._bases;

namespace SalesOrderManagement.API.Infra.Configurations
{
    public class UserConfiguration : BaseEntityConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            TableName = "Users";

            base.Configure(builder);


            builder.Property(u => u.FullName)
                .IsRequired()
                .HasMaxLength(Const.NAME_MAX_LENGTH);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(Const.EMAIL_MAX_LENGTH);

            builder.HasIndex(u => u.Email)
                .IsUnique();

            builder.Property(u => u.Phone)
                .HasMaxLength(Const.PHONE_MAX_LENGTH);

            builder.Property(u => u.Password)
                .IsRequired();

            builder.Property(u => u.Document)
                .HasMaxLength(Const.DOCUMENT_MAX_LENGTH);

            builder.Property(u => u.UserRole)
                .IsRequired()
                .HasConversion<string>(); 

            builder.Property(u => u.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            builder.HasMany(u => u.CreateByUser)
                .WithOne(o => o.CreateByUser)
                .HasForeignKey(o => o.CreateByUserUuid)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.ActionedOrders)
                .WithOne(o => o.ActionedByUser)
                .HasForeignKey(o => o.ActionedByUserUuid)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);
        }
    }
}
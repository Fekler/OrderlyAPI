using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SalesOrderManagement.Domain.Entities._bases;


namespace SalesOrderManagement.API.Infra.Configurations._bases
{
    public abstract class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : EntityBase
    {
        protected string TableName { get; set; } = string.Empty;
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(e => e.UUID);

            builder.Property(e => e.UUID)
                .IsRequired()
                .HasDefaultValueSql("SYS_GUID()");
            builder.Property(e => e.Id)
                 .ValueGeneratedOnAdd();
            builder.HasIndex(e => e.Id)
                 .IsUnique();
            builder.Property(e => e.CreateAt)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.UpdateAt)
                .IsRequired(false);

        }

    }
}



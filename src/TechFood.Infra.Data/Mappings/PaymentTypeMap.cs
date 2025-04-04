using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechFood.Domain.Entities;

namespace TechFood.Infra.Data.Mappings
{
    public class PaymentTypeMap : IEntityTypeConfiguration<PaymentType>
    {
        public void Configure(EntityTypeBuilder<PaymentType> builder)
        {
            builder.ToTable("TblPaymentType");

            builder.HasIndex(p => p.Code)
                .IsUnique();

            builder.Property(p => p.Code)
                .HasMaxLength(5)
                .IsRequired();

            builder.Property(p => p.Description)
                .HasMaxLength(15)
                .IsRequired();
        }
    }
}

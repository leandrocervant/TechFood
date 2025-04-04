using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechFood.Domain.Entities;
using TechFood.Infra.Data.ValueObjectMappings;

namespace TechFood.Infra.Data.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("TblUser");

            builder.OwnsOne(a => a.Address, address => address.MapAddress())
                .Navigation(a => a.Address);
        }
    }
}

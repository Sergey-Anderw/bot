using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    internal class OwnerEntityMap : IEntityTypeConfiguration<OwnerEntity>
    {
        public void Configure(EntityTypeBuilder<OwnerEntity> builder)
        {
            builder.ToTable("Owners", "dbo");
            builder
                .HasKey(o => o.Id);
            builder.Property(o => o.Id)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("newsequentialid()");
            builder.Property(o => o.FirstName).IsRequired();
            builder.Property(o => o.LastName).IsRequired();
            builder.Property(o => o.Email).IsRequired();
            builder.Property(o => o.PhoneNumber).IsRequired();

            builder.HasMany(o => o.Offers)
                .WithOne(oe => oe.Owner)
                .HasForeignKey(oe => oe.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(o => o.Bookings)
                .WithOne(be => be.Owner)
                .HasForeignKey(be => be.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

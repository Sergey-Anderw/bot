using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    internal class ConsumerEntityMap : IEntityTypeConfiguration<ConsumerEntity>
    {
        public void Configure(EntityTypeBuilder<ConsumerEntity> builder)
        {
            builder.ToTable("Consumers", "dbo");
            builder
                .HasKey(o => o.Id);
            builder.Property(o => o.Id)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("newsequentialid()");

            builder.Property(c => c.FirstName).IsRequired();
            builder.Property(c => c.LastName).IsRequired();
            builder.Property(c => c.Email).IsRequired();
            builder.Property(c => c.PhoneNumber).IsRequired();


            // Навигационные свойства
            builder.HasMany(c => c.Bookings)
                .WithOne(be => be.Consumer)
                .HasForeignKey(be => be.ConsumerId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}

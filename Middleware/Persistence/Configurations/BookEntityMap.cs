using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    internal class BookEntityMap : IEntityTypeConfiguration<BookEntity>
    {
        public void Configure(EntityTypeBuilder<BookEntity> builder)
        {
            builder.ToTable("Book", "dbo");
            builder
                .HasKey(o => o.Id);
            builder.Property(o => o.Id)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("newsequentialid()");

            builder.Property(b => b.StartTime).IsRequired();
            builder.Property(b => b.EndTime);
            builder.Property(b => b.OrderTime).IsRequired();
            builder.Property(b => b.IsConfirmed).IsRequired();

            // Навигационные свойства
            builder.HasOne(b => b.Consumer)
                .WithMany(c => c.Bookings)
                .HasForeignKey(b => b.ConsumerId)
                .OnDelete(DeleteBehavior.SetNull); 

            builder.HasOne(b => b.Owner)
                .WithMany(o => o.Bookings)
                .HasForeignKey(b => b.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    internal class OfferEntityMap : IEntityTypeConfiguration<OfferEntity>
    {
        public void Configure(EntityTypeBuilder<OfferEntity> builder)
        {
            builder.ToTable("Offers", "dbo");
            builder
                .HasKey(o => o.Id);
            builder.Property(o => o.Id)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("newsequentialid()");
            builder.Property(o => o.Description)
                .IsRequired();
            builder.Property(o => o.Price)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.HasOne(o => o.Owner)
                .WithMany()
                .HasForeignKey(o => o.OwnerId)
                .OnDelete(DeleteBehavior.Cascade); 

            builder.HasOne(o => o.Category)
                .WithMany()
                .HasForeignKey(o => o.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}

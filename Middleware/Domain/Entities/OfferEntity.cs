#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace Domain.Entities
{
    public class OfferEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public required string Description { get; set; }
        public decimal Price { get; set; }

        // Дополнительные поля по необходимости

        // Навигационные свойства
        public Guid OwnerId { get; set; }
        public virtual OwnerEntity Owner { get; set; }

        public Guid CategoryId { get; set; }
        public virtual CategoryEntity Category { get; set; }
    }
}

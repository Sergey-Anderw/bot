#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace Domain.Entities
{
    public class CategoryEntity
    {
        public Guid Id { get; set; }

        public Guid? ParentId { get; set; }
        public CategoryEntity? ParentCategory { get; set; }
        public Dictionary<string, string> NameTranslations { get; set; } = new();
        public virtual ICollection<CategoryEntity> Subcategories { get; set; }
        //public ICollection<OwnerEntity>? Owners { get; set; }
        //public ICollection<OfferEntity>? Offerings { get; set; }
    }
}

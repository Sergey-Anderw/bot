namespace Domain.Entities
{
    public class OwnerEntity
    {
        private HashSet<BookEntity> _bookEntities = [];
        private HashSet<OfferEntity> _offerEntities = [];


        public Guid Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }

        // Дополнительные поля по необходимости (например, описание бизнеса, расписание работы и т.д.)

        // Навигационные свойства
        public IReadOnlyCollection<OfferEntity> Offers
        {
            get => _offerEntities;
            set => _offerEntities = value.ToHashSet() ?? throw new ArgumentNullException(nameof(value));
        }
        public IReadOnlyCollection<BookEntity> Bookings
        {
            get => _bookEntities;
            set => _bookEntities = value.ToHashSet() ?? throw new ArgumentNullException(nameof(value));
        }
    }
}

namespace Domain.Entities
{
    public class ConsumerEntity
    {
        private HashSet<BookEntity> _bookEntities = [];

        public Guid Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }

        // Дополнительные поля по необходимости (например, адрес, фотография профиля и т.д.)


        public IReadOnlyCollection<BookEntity> Bookings
        {
            get => _bookEntities;
            set => _bookEntities = value.ToHashSet() ?? throw new ArgumentNullException(nameof(value));
        }

    }
}

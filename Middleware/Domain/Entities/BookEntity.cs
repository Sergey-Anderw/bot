namespace Domain.Entities
{
    public class BookEntity 
    {
        public Guid Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime OrderTime { get; set; }

        public bool IsConfirmed { get; set; }


        public Guid ConsumerId { get; set; }
        public virtual ConsumerEntity? Consumer { get; set; }

        public Guid OwnerId { get; set; }
        public virtual required OwnerEntity Owner { get; set; }

    }
}

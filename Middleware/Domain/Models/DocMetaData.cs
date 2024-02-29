using Domain.Enums;

namespace Domain.Models
{
    public class DocMetaData
    {
        public required string Name { get; set; }
        public required string DocumentType { get; set; }
        public required string Country { get; set; }
        public required string Keywords { get; set; }
        public required DateTime PublicationDate { get; set; }
        public AccessLevel AccessLevel { get; set; }
        public required string Language { get; set; }
    }
}

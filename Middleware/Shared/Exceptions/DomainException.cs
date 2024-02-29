using System.Runtime.Serialization;

namespace Master.Domain.Exceptions
{
    [Serializable]
    public class DomainException : Exception
    {
        public DomainException()
            : base("Unknown domain exception.")
        {
        }

        public DomainException(string message)
            : base(message)
        {
        }

        public DomainException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

    }
}

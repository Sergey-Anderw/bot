using System.Net;

namespace Shared.Exceptions
{
    [Serializable]
    public class BusinessException : Exception
    {
        public BusinessException(int code)
        {
            Code = code;
        }

        public BusinessException(int code, string message) : base(message)
        {
            Code = code;
        }

        public BusinessException(int code, string message, Exception innerException) : base(message, innerException)
        {
            Code = code;
        }

        public int Code { get; }
        public string Type { get; set; } = null!;
        public HttpStatusCode HttpStatusCode { get; set; }
        public IEnumerable<KeyValuePair<string, string[]>> Messages { get; set; } = null!;
    }
}
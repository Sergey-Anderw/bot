using System.Collections;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace Shared.Exceptions
{
    public class ExceptionInfo
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public string ExceptionType { get; set; }
        public string StackTrace { get; set; }
        public string InnerMessage { get; set; }
        public IDictionary Data { get; set; }
    }
}
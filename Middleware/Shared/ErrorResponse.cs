using System.Collections;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace Shared
{
    public class ErrorResponse
    {
        public required ResponseInfo Info { get; set; }

        public class ResponseInfo
        {
            public required string CodeMsg { get; set; }
            public required string StackTrace { get; set; }
            public string? Inner { get; set; }
            public IDictionary Data { get; set; }
        }
    }
}

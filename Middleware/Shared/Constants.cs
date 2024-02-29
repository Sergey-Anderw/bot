using System.Globalization;

namespace Shared
{
    public static class Constants
    {
        public const int MaxStringLength = 255;
        public const StringComparison DefaultStringComparison = StringComparison.InvariantCultureIgnoreCase;
        public static CultureInfo DefaultCultureInfo { get; } = new CultureInfo("en-US");
        public const string DateTimeStringFormat = "yyyy-MM-ddTHH:mm:ss.fffZ";
        public const string DefaultSalt = "SOME-SALT-STRING";

        public static class MediaTypeNames
        {
            public static class Application
            {
                public const string Json = "application/json";
            }
        }
    }
}
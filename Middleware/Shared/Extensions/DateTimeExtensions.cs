using Newtonsoft.Json;

namespace Shared.Extensions
{
    public static class DateTimeExtensions
    {
        private static readonly JsonSerializerSettings MicrosoftDateFormatSettings = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
            };

        public static string AsStringUtcIso8601(this DateTime dateTime)
        {
            return dateTime.ToUniversalTime().ToString("O");
        }

        public static string AsJsonString(this DateTime dateTime)
        {
            return JsonConvert.SerializeObject(dateTime, MicrosoftDateFormatSettings)
                .Replace("\"\\/Date(","/Date(")
                .Replace(")\\/\"", ")/");
        }
    }
}
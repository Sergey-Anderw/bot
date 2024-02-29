using Newtonsoft.Json;
using Shared.Helpers;

namespace Shared.Extensions
{
    public static class ObjectExtensions
    {
        public static Task<string> AsJsonStringAsync(this object obj, JsonSerializerSettings jsonSerializerSettings, CancellationToken cancellationToken)
        {
            return JsonConvertHelper.SerializeObjectAsync(obj, Formatting.Indented, jsonSerializerSettings, cancellationToken);
        }

        public static Task<string> AsJsonStringAsync(this object obj, CancellationToken cancellationToken)
        {
            return JsonConvertHelper.SerializeObjectAsync(obj, cancellationToken);
        }

        public static string AsJsonString(this object obj, JsonSerializerSettings jsonSerializerSettings)
        {
            return JsonConvertHelper.SerializeObject(obj, Formatting.Indented, jsonSerializerSettings);
        }

        public static string AsJsonString(this object obj)
        {
            return JsonConvertHelper.SerializeObject(obj);
        }
    }
}
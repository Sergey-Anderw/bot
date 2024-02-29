using Newtonsoft.Json;

namespace Shared.Helpers
{
    public static class JsonConvertHelper
    {
        public static T DeserializeObject<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value)!;
        }

        public static T DeserializeObject<T>(string value, JsonSerializerSettings jsonSerializerSettings)
        {
            return JsonConvert.DeserializeObject<T>(value, jsonSerializerSettings)!;
        }

        public static Task<T> DeserializeObjectAsync<T>(string value, CancellationToken cancellationToken)
        {
            return Task.Run(() => JsonConvert.DeserializeObject<T>(value), cancellationToken)!;
        }

        public static Task<T> DeserializeObjectAsync<T>(string value, JsonSerializerSettings jsonSerializerSettings, CancellationToken cancellationToken)
        {
            return Task.Run(() => JsonConvert.DeserializeObject<T>(value, jsonSerializerSettings), cancellationToken)!;
        }

        public static string SerializeObject<T>(T value)
        {
            return JsonConvert.SerializeObject(value);
        }

        public static string SerializeObject<T>(T value, Formatting formatting, JsonSerializerSettings jsonSerializerSettings)
        {
            return JsonConvert.SerializeObject(value, formatting, jsonSerializerSettings);
        }

        public static Task<string> SerializeObjectAsync<T>(T value)
        {
            return SerializeObjectAsync(value, CancellationToken.None);
        }

        public static Task<string> SerializeObjectAsync<T>(T value, CancellationToken cancellationToken)
        {
            return Task.Run(() => JsonConvert.SerializeObject(value), cancellationToken);
        }

        public static Task<string> SerializeObjectAsync<T>(T value, Formatting formatting, JsonSerializerSettings jsonSerializerSettings, CancellationToken cancellationToken)
        {
            return Task.Run(() => JsonConvert.SerializeObject(value, formatting, jsonSerializerSettings), cancellationToken);
        }
    }
}
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Shared.Configuration;
using Shared.Extensions;

namespace Shared.Helpers
{
    public class SecuredJsonConverter : JsonConverter
    {
        public static JsonConverter SuppressStateParsingConverter { get; }

        static SecuredJsonConverter()
        {
            SuppressStateParsingConverter = new SuppressStateParsing();
        }

        private class SuppressStateParsing : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return false;
            }

            public override object ReadJson(JsonReader reader, Type objectType, object? existingValue,
                JsonSerializer serializer)
            {
                throw new NotSupportedException();
            }

            public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
            {
                throw new NotSupportedException();
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object? existingValue,
            JsonSerializer serializer)
        {
            try
            {
                var token = JToken.ReadFrom(reader);
                var encrypted = token.Value<string>();

                if (encrypted == null)
                    return null!;
                

                if (SharedAppSettings.EncryptState)
                {
                    var text = encrypted.Decrypt(SharedAppSettings.CommunicationStateEncryptionKey!);
                    return JsonConvert.DeserializeObject(text, objectType)!;
                }

                return JsonConvert.DeserializeObject(encrypted, objectType)!;
            }
            catch
            {
                if (serializer.Converters.Any(x => x == SuppressStateParsingConverter))
                    return null!;
                throw;
            }
        }

        public override bool CanConvert(Type objectType)
        {
            return false;
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            if (SharedAppSettings.EncryptState)
            {
                var token = JToken.FromObject(JsonConvert.SerializeObject(value).Encrypt(SharedAppSettings.CommunicationStateEncryptionKey!));
                token.WriteTo(writer);
            }
            else
            {
                var token = JToken.FromObject(JsonConvert.SerializeObject(value));
                token.WriteTo(writer);
            }
        }
    }
}

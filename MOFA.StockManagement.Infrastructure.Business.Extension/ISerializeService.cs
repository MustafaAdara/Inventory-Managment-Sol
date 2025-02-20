using System.Text.Json;
using System.Text.Json.Serialization;

namespace MOFA.StockManagement.Infrastructure.Business.Extension
{
    public interface ISerializeService
    {
        public string SerializeJson<T>(T obj) where T : notnull;
        public T? DeserializeJson<T>(string record);
    }
    public class SerializeService : ISerializeService
    {
        public string SerializeJson<T>(T obj) where T : notnull
        {
            return JsonSerializer.Serialize<object>(obj, new JsonSerializerOptions()
            {
                Converters = { new DateTimeConverterUsingDateTimeParseHelper() },
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            });
        }
        public T? DeserializeJson<T>(string record)
        {
            try
            {
                return JsonSerializer.Deserialize<T>(record, new JsonSerializerOptions()
                {
                    Converters = { new DateTimeConverterUsingDateTimeParseHelper() },
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
                });
            }
            catch (Exception)
            {
                return default;
            }
        }
    }
    public class DateTimeConverterUsingDateTimeParseHelper : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.Parse(reader.GetString() ?? string.Empty);
            //return DateTime.ParseExact(reader.GetString() ?? string.Empty, "yyyy-MM-ddTHH:mm:ss.ffffZ", null);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            DateTime? dateTime = (DateTime?)value;
            var datestr = dateTime?.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ") ?? null;
            writer.WriteStringValue(datestr != null ? $"{datestr}" : null);
        }
    }
}

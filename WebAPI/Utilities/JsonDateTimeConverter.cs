using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Globalization;

namespace WebAPI.Utilities
{
    public class JsonDateTimeConverter : DateTimeConverterBase
    {
        public override bool CanConvert(Type objectType)
        {
            return base.CanConvert(objectType);
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            string? value = reader?.Value?.ToString();
            if (string.IsNullOrEmpty(value))
                return null;

            if (DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.None, out var result))
                return result;

            return null;
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            if (writer == null)
                return;

            if (value == null || ((DateTime)value) == default(DateTime))
            {
                writer.WriteValue("");
            }
            else
            {
                writer.WriteValue(((DateTime)value).ToString("yyyy/MM/dd HH:mm:ss"));
            }
        }
    }
}

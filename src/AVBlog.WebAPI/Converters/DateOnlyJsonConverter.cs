using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AVBlog.WebAPI.Converters
{
    public class DateOnlyJsonConverter : JsonConverter<DateOnly>
    {
        private readonly string _dateFormat = "dd-MM-yyyy";
        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateOnly.ParseExact(reader.GetString(), _dateFormat, CultureInfo.InvariantCulture);
        }

        public override void Write(Utf8JsonWriter writer, DateOnly dateTimeValue, JsonSerializerOptions options)
        {
            writer.WriteStringValue(dateTimeValue.ToString(_dateFormat, CultureInfo.InvariantCulture));
        }
    }
}

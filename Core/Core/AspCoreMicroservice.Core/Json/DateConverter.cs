using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AspCoreMicroservice.Core.Json
{
    public class DateConverter : JsonConverter<DateTime>
    {
        protected const string DateTimeFormat = "yyyy-MM-dd";
        protected const string FullDateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fffffffZ";
        protected const string LegacyDateTimeFormat = "yyyy-MM-dd HH:mm:ss.fffffffZ";

        public DateConverter()
        {
        }
     
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            DateTime value;
            if (reader.TryGetDateTime(out value))
                return value;

            var dateTimeAsString = reader.GetString();

            if (DateTime.TryParseExact(dateTimeAsString, DateTimeFormat, CultureInfo.CurrentCulture, DateTimeStyles.AssumeUniversal, out value))
                return value;

            if (DateTime.TryParseExact(dateTimeAsString, FullDateTimeFormat, CultureInfo.CurrentCulture, DateTimeStyles.AssumeUniversal, out value))
                return value;

            if (DateTime.TryParseExact(dateTimeAsString, LegacyDateTimeFormat, CultureInfo.CurrentCulture, DateTimeStyles.AssumeUniversal, out value))
                return value;

            return reader.GetDateTime();
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(DateTimeFormat));
        }       
    }
}

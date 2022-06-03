using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace eNotas.Sharp
{
	public class CustomDateTimeConverter : JsonConverter<DateTimeOffset>
	{
        public override void WriteJson(JsonWriter writer, DateTimeOffset value, JsonSerializer serializer)
        {
			writer.WriteValue(value.UtcDateTime.ToString("o"));
		}

        public override DateTimeOffset ReadJson(JsonReader reader, Type objectType, DateTimeOffset existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
			var dateUtc0 = (DateTime)reader.Value;

			var localZone = TimeZoneInfo.Local;

			var dateLocal = TimeZoneInfo.ConvertTimeFromUtc(dateUtc0, localZone);

			return dateLocal;
		}
    }
}

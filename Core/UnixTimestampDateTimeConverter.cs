using System.Text.Json;
using System.Text.Json.Serialization;

namespace Sinaf.Me;

public sealed class UnixTimestampDateTimeConverter : JsonConverter<DateTime>
{
	public override DateTime Read(
		ref Utf8JsonReader reader,
		Type typeToConvert,
		JsonSerializerOptions options)
	{
		var seconds = reader.GetInt64();
		return DateTimeOffset.FromUnixTimeSeconds(seconds).UtcDateTime;
	}
	
	public override void Write(
		Utf8JsonWriter writer,
		DateTime value,
		JsonSerializerOptions options)
	{
		var seconds = new DateTimeOffset(value.ToUniversalTime()).ToUnixTimeSeconds();
		writer.WriteNumberValue(seconds);
	}
}
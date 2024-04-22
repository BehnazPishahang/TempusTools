using System;
using System.Text.Json;
using System.Text.Json.Serialization;

public class DateOnlyConverter : JsonConverter<DateOnly>
{
    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String)
        {
            throw new WeatherForecastConflictException($"Expected string for Date Value but got {reader.TokenType}.");
        }

        if (!DateOnly.TryParse(reader.GetString(), out DateOnly date))
        {
            throw new WeatherForecastConflictException("Invalid date format.");
        }

        return date;
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}

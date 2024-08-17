using System.Text.Json;
using System.Text.Json.Serialization;

namespace Frank.Wpf.Core.Beatification;

public class JsonBeautifier : TextBeautifierBase
{
    public override string Beautify(string text) =>
        string.IsNullOrWhiteSpace(text)
            ? string.Empty
            : JsonSerializer.Serialize(JsonDocument.Parse(text).RootElement, new JsonSerializerOptions
            {
                WriteIndented = true,
                Converters = { new JsonStringEnumConverter() },
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            });
}
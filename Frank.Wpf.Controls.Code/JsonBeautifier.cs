using System.Text.Json;

namespace Frank.Wpf.Controls.Code;

public class JsonBeautifier : CodeBeautifierBase
{
    public override string Beautify(string code)
    {
        // Example using System.Text.Json for formatting JSON
        var jsonElement = JsonSerializer.Deserialize<JsonElement>(code);
        return JsonSerializer.Serialize(jsonElement, new JsonSerializerOptions
        {
            WriteIndented = true
        });
    }
}
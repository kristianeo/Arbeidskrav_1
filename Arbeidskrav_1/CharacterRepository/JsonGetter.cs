using System.Text.Json;

namespace Arbeidskrav_1.CharacterRepository;

public abstract class JsonGetter
{
    public static List<Dictionary<string, string>> GetJson()
    {
        string filePath = FilePathGetter.GetFilePath();

        List<Dictionary<string, string>> characters = [];

        if (File.Exists(filePath))
        {
            string existingJson = File.ReadAllText(filePath);

            if (!string.IsNullOrWhiteSpace(existingJson))
            {
                characters = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(existingJson)
                             ?? new List<Dictionary<string, string>>();
            }
        }
        return characters;
    }
}
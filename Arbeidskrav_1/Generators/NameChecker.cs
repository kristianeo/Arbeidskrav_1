using System.Text.Json;

namespace Arbeidskrav_1.Generators;

public abstract class NameChecker
{
    public static bool CharacterExists(string name)
    {
        const string filePath = 
            @"C:\Users\olsen\OneDrive\Dokumenter\GitHub\Arbeidskrav_1\Arbeidskrav_1\CharacterRepository\CharacterRepo.json";
        string json = File.ReadAllText(filePath);
        
        if (!File.Exists(filePath) || string.IsNullOrWhiteSpace(json))
            return false;

        var characters = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(json);

        return characters != null && characters.Any(c => c.ContainsValue(name));
    }
}
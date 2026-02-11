using System.Text.Json;
using Spectre.Console;

namespace Arbeidskrav_1.CharacterRepository;

public abstract class CharacterSetter
{
    /// <summary>
    /// Adds the character data to the character repository
    /// </summary>
    /// <param name="data">Character data templated using CreateDictionary()</param>
    public static void AddToRepo(Dictionary<string, string> data)
    {
        const string filePath = @"..\Arbeidskrav_1\CharacterRepository\CharacterRepo.json";
        
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

        characters.Add(data);

        string updatedJson = JsonSerializer.Serialize(
            characters,
            new JsonSerializerOptions { WriteIndented = true }
        );

        File.WriteAllText(filePath, updatedJson);
        AnsiConsole.MarkupLine("[green]Character is saved![/]");
    }
}
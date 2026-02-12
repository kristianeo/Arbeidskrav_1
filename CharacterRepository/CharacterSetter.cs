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
        var characters = JsonGetter.GetJson();
        string filePath = FilePathGetter.GetFilePath();

        characters.Add(data);

        string updatedJson = JsonSerializer.Serialize(
            characters,
            new JsonSerializerOptions { WriteIndented = true }
        );

        File.WriteAllText(filePath, updatedJson);
        AnsiConsole.MarkupLine("[green]Character saved![/]");
    }
}
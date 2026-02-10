using System.Text;
using System.Text.Json;
using Arbeidskrav_1.CharacterClasses;
using Spectre.Console;

namespace Arbeidskrav_1.CharacterRepository;

public abstract class CharacterGetter
{
    /// <summary>
    /// The player enters a character name to search for in the repository.
    /// Displays the data using DisplayCharacter.
    /// </summary>
    /// <exception cref="FileNotFoundException"></exception>
    public static void GetCharacter()
    {
        AnsiConsole.MarkupLine("[bold]Let's search for a character![/]\n");
        string name = AnsiConsole.Ask<string>("What is the name of the character?");
        
        const string filePath = @"C:\Users\olsen\OneDrive\Dokumenter\GitHub\Arbeidskrav_1\Arbeidskrav_1\CharacterRepository\CharacterRepo.json";
        string json = File.ReadAllText(filePath);
        
        if (!File.Exists(filePath) || string.IsNullOrWhiteSpace(json))
        {
            throw new FileNotFoundException("The file doesn't exist");
        }
        
        List<Dictionary<string, string>>? characters =
            JsonSerializer.Deserialize<List<Dictionary<string, string>>>(json);

        var info = characters?.FirstOrDefault(c => c.ContainsValue(name));
        if (info == null)
        {
            AnsiConsole.MarkupLine("[red]The character does not exist.[/]");
        }
        else
        {
            CharacterDisplayer.DisplayCharacter(info);
        }
        UserInterface.CreateOrSearch.Choice();
    }
}
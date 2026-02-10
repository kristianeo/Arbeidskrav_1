using System.Text.Json;
using Arbeidskrav_1.CharacterClasses;
using Spectre.Console;

namespace Arbeidskrav_1.CharacterRepository;

public abstract class CharacterGetter
{
    public static void GetCharacter()
    {
        AnsiConsole.MarkupLine("[Blue]Let's search for a character![/]");
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
            DisplayCharacter(info);
        }
        UserInterface.CreateOrSearch.Choice();
    }

    private static void DisplayCharacter(Dictionary<string, string> info)
    {
        foreach (string key in info.Keys)
        {
            AnsiConsole.MarkupLine($"[bold]{key}:[/] {info[key]}");
        }
            
           // TODO: Make this more esthetic if there is time 
    }

}
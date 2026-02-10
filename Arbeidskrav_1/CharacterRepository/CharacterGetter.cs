using System.Text;
using System.Text.Json;
using Arbeidskrav_1.CharacterClasses;
using Spectre.Console;

namespace Arbeidskrav_1.CharacterRepository;

public abstract class CharacterGetter
{
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
            DisplayCharacter(info);
        }
        UserInterface.CreateOrSearch.Choice();
    }

    private static void DisplayCharacter(Dictionary<string, string> info)
    {
        
        var grid = new Grid();
  
        grid.AddColumn(new GridColumn { Width = 20, Alignment = Justify.Right });
        grid.AddColumn(new GridColumn());
  
        grid.AddEmptyRow();
  
        grid.AddRow(new Markup("[bold]Character Name: [/]"), new Markup(info["Character Name: "]));
        grid.AddRow(new Markup("[bold]Class: [/]"), new Markup(info["Class: "]));
        grid.AddRow(new Markup("[bold]Hit Points: [/]"), new Markup(info["Hit Points: "]));
        grid.AddRow(new Markup("[bold]Prime Requisite: [/]"), new Markup(info["Prime Requisite: "] + ": " + info["Prime requisite score: "] + " (Modifier " + info["Modifier: "] + ")"));
        grid.AddRow(new Markup("[bold]XP for level 2: [/]"), new Markup( info["XP for level 2: "]));
        grid.AddRow(
            new Markup("[bold]Ability Scores: [/]"),
            new Panel($"[yellow]STR: [/][yellow4]{info["Strength"]} [/]" +
                      $"[green3]INT: [/][green4]{info["Intelligence"]} [/]" +
                      $"[blue]WIS: [/][blue1]{info["Wisdom"]} [/]")
                .NoBorder());
        grid.AddRow(
            new Markup(""),
            new Panel($"[red]DEX: [/][red3]{info["Dexterity"]}[/] " +
                      $"[darkorange]CON: [/][darkorange3_1]{info["Constitution"]} [/]" +
                      $"[purple]CHA: [/][purple4]{info["Charisma"]} [/]")
                .NoBorder());
  
        AnsiConsole.Write(grid);
    }

}
using Spectre.Console;
using Spectre.Console.Rendering;

namespace Arbeidskrav_1.CharacterRepository;

public abstract class CharacterGetter
{
    /// <summary>
    /// The player enters a character name to search for in the repository,
    /// or show all created characters.
    /// Displays the data using DisplayCharacter.
    /// </summary>
    public static void GetCharacter()
    {
        var characters = JsonGetter.GetJson();
        
        var prompt = new SelectionPrompt<string>()
            .Title("\n[bold]What would you like to do?[/]")
            .AddChoices("Search for a character", "Show all generated characters");
        var selected = AnsiConsole.Prompt(prompt);
        
        if (characters.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]There are no characters in the repository.[/]");
        }

        else if (selected == "Show all generated characters")
        {
            Console.Clear();
            var rows = new List<IRenderable>();
            
            foreach (var character in characters)
            {
                rows.Add(CharacterDisplayer.DisplayCharacter(character));
            }
            
            var panel = new Panel(new Rows(rows))
                .Header("Character Repository")
                .DoubleBorder()
                .BorderColor(Color.Green);

            AnsiConsole.Write(panel);
        }
        else
        {
            Console.Clear();
            AnsiConsole.MarkupLine("[bold]Let's search for a character![/]\n");
            string name = AnsiConsole.Ask<string>("What is the name of the character?");

            var info = characters.FirstOrDefault(c => c.ContainsValue(name));
            if (info == null)
            {
                AnsiConsole.MarkupLine("[red]The character does not exist.[/]");
            }
            else
            {
                var grid = CharacterDisplayer.DisplayCharacter(info);
                AnsiConsole.Write(grid);
            }
        }
        UserInterface.CreateOrSearch.Choice();
    }
}
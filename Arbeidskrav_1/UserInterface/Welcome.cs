using Spectre.Console;

namespace Arbeidskrav_1.UserInterface;

public abstract class Welcome
{
    /// <summary>
    /// Shows the starting screen. The player gets to chose if they want to
    /// make a new character or search in the repository.
    /// </summary>
    public static void WelcomeMessage()
    {
        Console.Clear();
        var figlet = new FigletText("Welcome")
        {
            Color = Color.Green,
            Justification = Justify.Center
        };
        
        AnsiConsole.Write(figlet);
        AnsiConsole.Write(new Rule("[green]to the character generator![/]"));
        
        var prompt = new SelectionPrompt<string>()
            .Title("\n[rapidblink bold]What would you like to do?[/]")
            .AddChoices("Create character", "Search in character repository");
        var selected = AnsiConsole.Prompt(prompt);
        AnsiConsole.MarkupLineInterpolated($"\n[blue]You selected[/] {selected} ");
        
        switch (selected)
        {
            case "Create character":
                AnsiConsole.Clear();
                RunGenerator.Run();
                break;
            case "Search in character repository":
                AnsiConsole.Clear();
                CharacterRepository.CharacterGetter.GetCharacter();
                break;
        }
    }
}
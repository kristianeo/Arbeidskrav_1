using Spectre.Console;

namespace Arbeidskrav_1.UserInterface;

public class Welcome
{
    public static void WelcomeMessage()
    {
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine("[green]Welcome to the Character Generator! [/]");
        var prompt = new SelectionPrompt<string>()
            .Title("\n[red]What would you like to do?[/]")
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
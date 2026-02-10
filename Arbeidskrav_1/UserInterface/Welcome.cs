using Spectre.Console;

namespace Arbeidskrav_1.UserInterface;

public class Welcome
{
    public static void WelcomeMessage()
    {
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
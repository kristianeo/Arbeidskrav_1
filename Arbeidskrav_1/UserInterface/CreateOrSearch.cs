using System.Net.Mime;
using Arbeidskrav_1.CharacterRepository;
using Spectre.Console;

namespace Arbeidskrav_1.UserInterface;

public class CreateOrSearch
{
    public static void Choice()
    {
        var prompt = new SelectionPrompt<string>()
            .Title("\n[purple]What would you like to do next?[/]")
            .AddChoices("Create a new character", "Search in character repository", "Exit");
        var selected = AnsiConsole.Prompt(prompt);
        switch (selected)
        {
            case "Create a new character":
                RunGenerator.Run();
                break;
            case "Search in character repository":
                CharacterGetter.GetCharacter();
                break;
            case "Exit":
                Environment.Exit(0);
                break;
        }
    }
}
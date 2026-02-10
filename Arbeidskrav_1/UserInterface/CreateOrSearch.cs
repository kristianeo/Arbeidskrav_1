using System.Net.Mime;
using Arbeidskrav_1.CharacterRepository;
using Spectre.Console;

namespace Arbeidskrav_1.UserInterface;

public abstract class CreateOrSearch
{
    /// <summary>
    /// Prompts the player what to do next. Gets called after creating a character or searching
    /// the repository.
    /// </summary>
    public static void Choice()
    {
        var prompt = new SelectionPrompt<string>()
            .Title("\n[bold rapidblink]What would you like to do next?[/]")
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
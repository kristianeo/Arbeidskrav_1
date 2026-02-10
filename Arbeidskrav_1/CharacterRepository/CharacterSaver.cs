using Arbeidskrav_1.CharacterClasses;
using Spectre.Console;

namespace Arbeidskrav_1.CharacterRepository;

public class CharacterSaver
{
    public static void SaveCharacter(CharacterClass character, string hitPoints)
    {
        var data = CharacterSetter.CreateDictionary(character, hitPoints);
        var prompt = new SelectionPrompt<string>()
            .Title("\n[red]Would you like to save this character?[/]")
            .AddChoices("Yes", "No");
        var selected = AnsiConsole.Prompt(prompt);
        switch (selected)
        {
            case "Yes":
                CharacterSetter.AddToRepo(data);
                break;
        }
        UserInterface.CreateOrSearch.Choice();
    }
}
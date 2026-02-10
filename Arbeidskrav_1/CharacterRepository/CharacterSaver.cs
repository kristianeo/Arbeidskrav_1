using Arbeidskrav_1.CharacterClasses;
using Spectre.Console;

namespace Arbeidskrav_1.CharacterRepository;

public abstract class CharacterSaver
{
    public static void SaveCharacter(CharacterClass character, string hitPoints)
    {
        Dictionary<string, string> data = CharacterSetter.CreateDictionary(character, hitPoints);
        var prompt = new SelectionPrompt<string>()
            .Title("\n[red]Would you like to save this character?[/]")
            .AddChoices("Yes", "No");
        string selected = AnsiConsole.Prompt(prompt);
        if (selected == "Yes")
        {
            CharacterSetter.AddToRepo(data);
        }
        UserInterface.CreateOrSearch.Choice();
    }
}
using Arbeidskrav_1.CharacterClasses;
using Spectre.Console;

namespace Arbeidskrav_1.CharacterRepository;

public abstract class CharacterSaver
{
    /// <summary>
    /// Prompts the user if they want to save the character they just created.
    /// If the player prompts yes, it calls on CreateDictionary() to template the data
    /// and calls AddToRepo() with said data.
    /// </summary>
    /// <param name="character"></param>
    /// <param name="hitPoints"></param>
    public static void SaveCharacter(CharacterClass character, string hitPoints)
    {
        var prompt = new SelectionPrompt<string>()
            .Title("\n[bold]Would you like to save this character?[/]")
            .AddChoices("Yes", "No");
        string selected = AnsiConsole.Prompt(prompt);
        if (selected == "Yes")
        {
            Dictionary<string, string> data = DictionaryCreator.CreateDictionary(character, hitPoints);
            CharacterSetter.AddToRepo(data);
        }
        UserInterface.CreateOrSearch.Choice();
    }
}
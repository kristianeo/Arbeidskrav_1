using Arbeidskrav_1.CharacterClasses;
using Spectre.Console;

namespace Arbeidskrav_1.Generators;

public class UserPrompts
{
    
    /// <summary>
    /// The player enters a name.
    /// </summary>
    /// <returns>Character name</returns>
    public static string ChooseCharacterName()
    {
        var charName = AnsiConsole.Ask<string>("[blue]Enter character name: [/]");
        
        return charName;
    }
    
    
}
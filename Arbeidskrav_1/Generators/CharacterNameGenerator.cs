using Spectre.Console;

namespace Arbeidskrav_1.Generators;

public abstract class CharacterNameGenerator
{
    /// <summary>
    /// Lets the user enter a character name. Prompts for a valid input until given.
    /// </summary>
    /// <returns>Valid name chosen by player</returns>
    public static string GetCharacterName()
    {
        AskName:
        var charName = AnsiConsole.Ask<string>("[bold]Enter character name: [/]");
        
        if (NameChecker.CharacterExists(charName))
        {
            AnsiConsole.MarkupLine("[red]The character name is already in use.[/]");
            goto AskName;
        }
        
        if (charName.Length is < 3 or > 35)
        {
            AnsiConsole.MarkupLine("[red]Character name must be between 3 and 15 characters long.[/]");
            goto AskName;
        }

        foreach (char item in charName)
        {                        
            if (!char.IsAsciiLetterOrDigit(item))
            {
                AnsiConsole.MarkupLine("[red]Character name can only contain letters A-Z and numbers 0-9[/]");
                goto AskName;
            }
        }
        return charName;
    } 
}
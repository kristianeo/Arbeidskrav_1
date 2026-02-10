using Spectre.Console;

namespace Arbeidskrav_1.Generators;

public abstract class CharacterNameGenerator
{
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
            if (!char.IsAsciiLetter(item))
            {
                AnsiConsole.MarkupLine("[red]Character name can only contain letters A-Z[/]");
                goto AskName;
            }
        }
        return charName;
    } 
}
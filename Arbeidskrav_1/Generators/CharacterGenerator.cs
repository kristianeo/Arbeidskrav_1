using Arbeidskrav_1.CharacterClasses;
using Spectre.Console;

namespace Arbeidskrav_1.Generators;

public abstract class CharacterGenerator
{
    public static CharacterClass GenerateClass(string? classChoice)
    {
        string charName = GetCharacterName();
        string? choice = classChoice?.ToLower();
        switch (choice)
        {
            case "cleric":
                Cleric cleric = new Cleric(charName, CharacterClass.AbilityScores);
                return cleric;

            case "fighter":
                Fighter fighter = new Fighter(charName, CharacterClass.AbilityScores);
                return fighter;

            case "thief":
                Thief thief = new Thief(charName, CharacterClass.AbilityScores);
                return thief;

            case "magic user":
                MagicUser magicUser = new MagicUser(charName, CharacterClass.AbilityScores);
                return magicUser;
            
            default: //This is never going to run
                Console.WriteLine("Something went wrong.");
                classChoice = ChooseClass.Choose();
                return GenerateClass(classChoice);
        }
    }

    private static string GetCharacterName()
    {
        AskName:
        var charName = AnsiConsole.Ask<string>("[blue]Enter character name: [/]");
    
        if (charName == "" || charName.Length < 3 || charName.Length > 15)
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
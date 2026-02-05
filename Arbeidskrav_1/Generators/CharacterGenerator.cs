using Arbeidskrav_1.CharacterClasses;
using Spectre.Console;

namespace Arbeidskrav_1.Generators;

public abstract class CharacterGenerator
{
    public static CharacterClass GenerateClass(string? classChoice)
    {
        var charName = AnsiConsole.Ask<string>("[blue]Enter character name: [/]");
        
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
            
            default:
                Console.WriteLine("Invalid choice");
                classChoice = ChooseClass.Choose();
                return GenerateClass(classChoice);
        }
    }
}
using System.Text.Json;
using Arbeidskrav_1.CharacterClasses;
using Spectre.Console;

namespace Arbeidskrav_1.Generators;

public abstract class CharacterGenerator
{
    /// <summary>
    /// Generates a new instance of concrete class
    /// </summary>
    /// <param name="classChoice"></param>
    /// <returns></returns>
    public static CharacterClass GenerateClass(string? classChoice)
    {
        string charName = CharacterNameGenerator.GetCharacterName();
        string? choice = classChoice;
        switch (choice)
        {
            case "Cleric":
                Cleric cleric = new Cleric(charName, CharacterClass.AbilityScores);
                return cleric;

            case "Fighter":
                Fighter fighter = new Fighter(charName, CharacterClass.AbilityScores);
                return fighter;

            case "Thief":
                Thief thief = new Thief(charName, CharacterClass.AbilityScores);
                return thief;

            case "Magic-User":
                MagicUser magicUser = new MagicUser(charName, CharacterClass.AbilityScores);
                return magicUser;
            default:
                throw new ArgumentOutOfRangeException(classChoice);
        }
    }
}
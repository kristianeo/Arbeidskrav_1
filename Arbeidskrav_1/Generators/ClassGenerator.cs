using Arbeidskrav_1.CharacterClasses;

namespace Arbeidskrav_1.Generators;

public class ClassGenerator
{
    public static CharacterClass GenerateClass(string classChoice, string charName)
    {
        string choice = classChoice.ToLower();
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
                return null;
        }
    }
}
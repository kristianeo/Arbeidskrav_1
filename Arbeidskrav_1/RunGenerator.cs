using Arbeidskrav_1.CharacterClasses;
using Arbeidskrav_1.Generators;

namespace Arbeidskrav_1;

public class RunGenerator
{
    public static void Run()
    {
        AbilityScoresGenerator.GenerateAbilityScores();
        AvailableClasses.CalculateHighestScores();
        AvailableClasses.ClassSelector();
        AvailableClasses.NoAvailableClassesCheck();
        CharacterClass character = CharacterGenerator.GenerateClass(ChooseClass.Choose());
        
        character.DisplayCharacter();

        UserInterface.UserInterface.DisplayCharacter(character);
    }
}
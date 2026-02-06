using Arbeidskrav_1.CharacterClasses;
using Arbeidskrav_1.Generators;

namespace Arbeidskrav_1;

public class RunGenerator
{
    public static void Run()
    {
        AbilityScores.AbilityScoreGenerator();
        AvailableClasses.CalculateHighestScores();
        AvailableClasses.NoAvailableClassesCheck();
        AvailableClasses.ClassSelector();
        CharacterClass character = CharacterGenerator.GenerateClass(ChooseClass.Choose());

        UserInterface.UserInterface.DisplayCharacter(character);
    }
}
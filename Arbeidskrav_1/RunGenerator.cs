using System.Text;
using Arbeidskrav_1.CharacterClasses;
using Arbeidskrav_1.CharacterRepository;
using Arbeidskrav_1.Generators;
using Arbeidskrav_1.UserInterface;

namespace Arbeidskrav_1;

public class RunGenerator
{
    public static void Run()
    {
        AbilityScoresGenerator.GenerateAbilityScores();
        AvailableClassesGenerator.AvailableClasses();
        CharacterClass character = CharacterGenerator.GenerateClass(ClassSelector.ChooseClass());
        UserInterface.CharacterDisplayer.DisplayCharacter(character);
    }
}

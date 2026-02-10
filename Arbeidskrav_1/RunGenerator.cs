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
        AvailableClasses.CalculateHighestScores();
        AvailableClasses.ClassSelector();
        AvailableClasses.NoAvailableClassesCheck();
        CharacterClass character = CharacterGenerator.GenerateClass(ChooseClass.Choose());
        UserInterface.UserInterface.DisplayCharacter(character);
        
        
    }
        
}

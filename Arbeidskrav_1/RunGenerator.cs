using System.Text;
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
        
        string hitPoints = character.DisplayCharacter();
        Dictionary<string, string> data = SaveCharacter.CreateDictionary(character, hitPoints);
        SaveCharacter.AddToRepo(data);
        Dictionary<string, string> info = CharacterRepository.CharacterGetter.GetCharacter();
        if (info != null)
        {
            foreach (string key in info.Keys)
            {
                Console.WriteLine($"{key}: {info[key]}");
            }
            
           
        }
        
    
        // UserInterface.UserInterface.DisplayCharacter(character);
    }
}
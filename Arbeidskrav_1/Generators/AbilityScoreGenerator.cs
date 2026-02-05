using Arbeidskrav_1.CharacterClasses;
using Spectre.Console;

namespace Arbeidskrav_1.Generators;

public abstract class AbilityScoreGenerator
{
    private static Dictionary<string, int> _abilityScores = new()
    {
        { "Strength", 0 },
        { "Intelligence", 0 },
        { "Wisdom", 0 },
        { "Dexterity", 0 },
        { "Constitution", 0 },
        { "Charisma", 0 }
    };
    
    
    /// <summary>
    /// Generates ability scores for each ability by rolling 3d6.
    /// Minimum value 3, maximum value 18.
    /// Uses Average(), if average is less than or equal to 8
    /// the player gets a chance to reroll.
    /// </summary>
    public static void AbilityScore()
    {
        TryAgain:
        foreach (string value in _abilityScores.Keys)
        {
            _abilityScores[value] = 0;
            _abilityScores[value] += DiceRoll.RollDice(3, 6);
            CharacterClass.AbilityScores[value] =  _abilityScores[value];
        }
        
        UserInterface.UserInterface.DisplayAbilityScores();
        
        if (Average(CharacterClass.AbilityScores) <= 8)
        {
            Console.WriteLine();
            if (AnsiConsole.Confirm("Your ability scores are below average. " +
                                    "\nWould you like to reroll?"))
            {
                Console.Clear();
                AnsiConsole.Status()
                    .Spinner(Spinner.Known.Arrow)
                    .Start("Rerolling...", ctx =>
                    {
                        Thread.Sleep(1500);
                    });
  
                AnsiConsole.MarkupLine("[green]Rerolled!![/]");
                goto TryAgain;
            }
            
        }
    }
    public static int Average(Dictionary<string, int> dictionary)
    {
        int total = 0;
        foreach (string value in dictionary.Keys)
        {
            total += dictionary[value];
        }

        int average = total / dictionary.Count;
        return average;
    }
}
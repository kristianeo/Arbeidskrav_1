using Arbeidskrav_1.CharacterClasses;
using Spectre.Console;

namespace Arbeidskrav_1.Generators;

public abstract class AbilityScoresGenerator
{
    /// <summary>
    /// Generates ability score per ability by rolling 3d6.
    /// Minimum value 3, maximum value 18.
    /// Calculates average score using Average(), if average is less than or equal to 8
    /// the player gets a choice to reroll by calling UnderAverageHandler().
    /// </summary>
    public static void GenerateAbilityScores()
    {
        Console.Clear();
        AnsiConsole.Status()
            .Spinner(Spinner.Known.Binary)
            .Start("Rolling ability scores...", ctx => { Thread.Sleep(1500); });
        
        foreach (string value in CharacterClass.AbilityScores.Keys)
        {
            CharacterClass.AbilityScores[value] = 0;
            CharacterClass.AbilityScores[value] += DiceRoll.RollDice("3d6");
            if (CharacterClass.AbilityScores[value] < 3 || CharacterClass.AbilityScores[value] > 18)
            {
                AnsiConsole.MarkupLine($"[red]{CharacterClass.AbilityScores[value]} is out of range.[/]");
            }
        }

        UserInterface.AbilityScoreDisplayer.DisplayAbilityScores();
        if (AverageGenerator.Average(CharacterClass.AbilityScores) <= 8)
        {
            UnderAverageHandler.UnderAverage();
        }
    }


   
  
}
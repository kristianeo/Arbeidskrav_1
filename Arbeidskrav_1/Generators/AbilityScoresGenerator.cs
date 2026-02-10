using Arbeidskrav_1.CharacterClasses;
using Spectre.Console;

namespace Arbeidskrav_1.Generators;

public abstract class AbilityScoresGenerator
{
    /// <summary>
    /// Generates ability score per ability by rolling 3d6.
    /// Minimum value 3, maximum value 18.
    /// Uses Average(), if average is less than or equal to 8
    /// the player gets a choice to reroll.
    /// </summary>
    public static void GenerateAbilityScores()
    {
        Console.Clear();
        AnsiConsole.Status()
            .Spinner(Spinner.Known.Arrow)
            .Start("Rolling ability scores...", ctx => { Thread.Sleep(1500); });

        foreach (string value in CharacterClass.AbilityScores.Keys)
        {
            CharacterClass.AbilityScores[value] = 0;
            CharacterClass.AbilityScores[value] += DiceRoll.RollDice("3d6");
        }

        UserInterface.UserInterface.DisplayAbilityScores();
        if (Average(CharacterClass.AbilityScores) <= 8)
        {
            UnderAverageHandler();
        }
    }

    private static void UnderAverageHandler()
    {
        var prompt = new SelectionPrompt<string>()
            .Title("\n[bold red]Your ability scores are below average... [/]" +
                   "\nWould you like to reroll?")
            .AddChoices("Yes", "No");
        var selected = AnsiConsole.Prompt(prompt);

        if (selected != "Yes") return;
        GenerateAbilityScores();

    }
    public static int Average(Dictionary<string, int> dictionary)
    {
        int total = dictionary.Keys.Sum(value => dictionary[value]);
        int average = total / dictionary.Count;
        return average;
    }
  
}
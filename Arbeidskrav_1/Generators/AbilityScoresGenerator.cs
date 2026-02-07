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
    public static void GenerateAbilityScores() // TODO: Add "roll ability scores"/view characters (after DB implement)
    {
        Console.Clear();
        foreach (string value in CharacterClass.AbilityScores.Keys)
        {
            CharacterClass.AbilityScores[value] = 0;
            CharacterClass.AbilityScores[value] += DiceRoll.RollDice(3, 6);
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
        AnsiConsole.MarkupLineInterpolated($"\n[blue]You selected[/] {selected} ");

        if (selected == "Yes")
        {
            Console.Clear();
            AnsiConsole.Status()
                .Spinner(Spinner.Known.Arrow)
                .Start("Rerolling...", ctx => { Thread.Sleep(1500); });

            AnsiConsole.MarkupLine("[green]Rerolled!![/]");
            Thread.Sleep(1500);
            GenerateAbilityScores();
        }
        
    }
        
   // TODO: add separate method for values below average 
    public static int Average(Dictionary<string, int> dictionary)
    {
        int total = dictionary.Keys.Sum(value => dictionary[value]);
        int average = total / dictionary.Count;
        return average;
    }
  
}
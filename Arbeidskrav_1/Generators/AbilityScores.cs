using Arbeidskrav_1.CharacterClasses;
using Spectre.Console;

namespace Arbeidskrav_1.Generators;

public abstract class AbilityScores
{
   /// <summary>
    /// Generates ability score per ability by rolling 3d6.
    /// Minimum value 3, maximum value 18.
    /// Uses Average(), if average is less than or equal to 8
    /// the player gets a choice to reroll.
    /// </summary>
    public static void AbilityScoreGenerator() // TODO: Add "roll ability scores"
    {
        TryAgain:
        Console.Clear();
        foreach (string value in CharacterClass.AbilityScores.Keys)
        {
            CharacterClass.AbilityScores[value] = 0;
            CharacterClass.AbilityScores[value] += DiceRoll.RollDice(3, 6);
        }
        
        UserInterface.UserInterface.DisplayAbilityScores();
        
        if (Average(CharacterClass.AbilityScores) <= 8)
        {
            var prompt = new SelectionPrompt<string>()
                .Title("\n[bold red]Your ability scores are below average... [/]" +
                       "\nWould you like to reroll? (y/n) ")
                .AddChoices("Yes", "No");
            var selected = AnsiConsole.Prompt(prompt);
            AnsiConsole.MarkupLineInterpolated($"\n[blue]You selected[/] {selected} ");
            
            if (selected == "Yes")
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
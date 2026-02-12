using Spectre.Console;

namespace Arbeidskrav_1.Generators;

public abstract class UnderAverageHandler
{
    /// <summary>
    /// Gets called when the ability score average is equal to or less than 8.
    /// The player can choose to reroll.
    /// </summary>
    public static void UnderAverage()
    {
        var prompt = new SelectionPrompt<string>()
            .Title("\n[bold red]Your ability scores are below average... [/]" +
                   "\nWould you like to reroll?")
            .AddChoices("Yes", "No");
        var selected = AnsiConsole.Prompt(prompt);

        if (selected != "Yes") return;
        AbilityScoresGenerator.GenerateAbilityScores();

    }
}
using Arbeidskrav_1.CharacterClasses;
using Arbeidskrav_1.Generators;
using Spectre.Console;

namespace Arbeidskrav_1.UserInterface;

public abstract class AbilityScoreDisplayer
{
    public static void DisplayAbilityScores()
    {
        var abilityScores = CharacterClass.AbilityScores;
        int average = AbilityScoresGenerator.Average(abilityScores);
        var abilityKeys = abilityScores.Keys.ToList();
        var abilityValues = abilityScores.Values.ToList();
        var chart = new BarChart()
            .Label("[bold]Ability Scores[/]")
            .WithMaxValue(25)
            .AddItem(abilityKeys[0], abilityValues[0], Color.Red)
            .AddItem(abilityKeys[1], abilityValues[1], Color.Blue)
            .AddItem(abilityKeys[2], abilityValues[2], Color.Green)
            .AddItem(abilityKeys[3], abilityValues[3], Color.Yellow)
            .AddItem(abilityKeys[4], abilityValues[4], Color.Orange1)
            .AddItem(abilityKeys[5], abilityValues[5], Color.Purple)
            .AddItem("", 0, Color.Black)
            .AddItem("Average", average, Color.Red3);

        AnsiConsole.Write(chart);
    }
}
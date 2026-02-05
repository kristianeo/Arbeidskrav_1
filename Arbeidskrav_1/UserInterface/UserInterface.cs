using Arbeidskrav_1.CharacterClasses;
using Arbeidskrav_1.Generators;

using Spectre.Console;

namespace Arbeidskrav_1.UserInterface;

public class UserInterface
{

    /// <summary>
    /// Displays stats for chosen character.
    /// Uses Modifier() and ConstitutionModifier() for specific class.
    /// </summary>
    /// <param name="character">character to display</param>
    public static void DisplayCharacter(CharacterClass character, string hitPoints)
    {
        var prScore = CharacterClass.AbilityScores.FirstOrDefault(s => s.Key == character.PrimeRequisite);
        string panelText = $"\n[bold]Name:[/] {character.CharacterName}" +
                           $"\n[bold]Class[/]: {character.ClassName}" +
                           $"\n[bold]Hit Points:[/] {hitPoints}";

        var display = new Panel(panelText)

            .Header("[yellow]Character created[/]", Justify.Center)
            .RoundedBorder()
            .BorderColor(Color.Yellow)
            .Border(BoxBorder.Ascii)
            .Padding(2, 1)
            .Expand();

        AnsiConsole.Write(display);



        Console.WriteLine("\nAbility Scores:");
        foreach (KeyValuePair<string, int> kvp in CharacterClass.AbilityScores)
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
        }

        Console.WriteLine($"\nPrime Requisite: {character.PrimeRequisite} ({prScore.Value}) - " +
                          $"Modifier: {Modifier.Modify(prScore.Value)}" +
                          $"\nXP for level 2: {character.XpLevel2}");
    }


    public static void DisplayAbilityScores()
    {
        var abilityScores = CharacterClass.AbilityScores;
        int average = AbilityScoreGenerator.Average(abilityScores);
        var abilityKeys = abilityScores.Keys.ToList();
        var abilityValues = abilityScores.Values.ToList();
        var chart = new BarChart()
            .Label("[bold]Ability Scores[/]")
            .WithMaxValue(12)
            .AddItem(abilityKeys[0], abilityValues[0], Color.Red)
            .AddItem(abilityKeys[1], abilityValues[1], Color.Blue)
            .AddItem(abilityKeys[2], abilityValues[2], Color.Green)
            .AddItem(abilityKeys[3], abilityValues[3], Color.Yellow)
            .AddItem(abilityKeys[4], abilityValues[4], Color.Orange1)
            .AddItem(abilityKeys[5], abilityValues[5], Color.Purple)
            .AddItem("Average", average, Color.Red3);

        AnsiConsole.Write(chart);
    }
}
// }
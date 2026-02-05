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
    public static void DisplayCharacter(CharacterClass character)
    {
        Console.Clear();
        var prScore = CharacterClass.AbilityScores.FirstOrDefault(s => s.Key == character.PrimeRequisite);
        var abilityValues = CharacterClass.AbilityScores.Values.ToList();
        
        AnsiConsole.Status()
            .Start("Creating character...", ctx =>
            {
                Thread.Sleep(1500);
  
                ctx.Status("Calculating scores...");
                Thread.Sleep(2000);
  
                // ctx.Status("Pouring into cup...");
                // Thread.Sleep(1000);
            });
  
        AnsiConsole.MarkupLine("[green]Character is created![/]");
        
        var grid = new Grid();
  
        grid.AddColumn(new GridColumn { Width = 20, Alignment = Justify.Right });
        grid.AddColumn(new GridColumn());
  
        grid.AddEmptyRow();
  
        grid.AddRow(new Markup("[bold]Character Name: [/]"), new Markup(character.CharacterName));
        grid.AddRow(new Markup("[bold]Class: [/]"), new Markup(character.ClassName));
        grid.AddRow(new Markup("[bold]Hit Points: [/]"), new Markup(character.ConstitutionModifier()));
        grid.AddRow(new Markup("[bold]Prime Requisite: [/]"), new Markup(character.PrimeRequisite + " (Modifier " + Modifier.Modify(prScore.Value) + ")"));
        grid.AddRow(new Markup("[bold]XP for level 2: [/]"), new Markup( $"{character.XpLevel2}"));
        grid.AddRow(
            new Markup("[bold]Ability Scores: [/]"),
            new Panel($"[yellow]STR: [/][yellow4]{abilityValues[0]} [/]" +
                      $"[green3]INT: [/][green4]{abilityValues[1]} [/]" +
                      $"[blue]WIS: [/][blue1]{abilityValues[2]} [/]")
                .NoBorder());
        grid.AddRow(
            new Markup(""),
            new Panel($"[red]DEX: [/][red3]{abilityValues[3]}[/] " +
                      $"[darkorange]CON: [/][darkorange3_1]{abilityValues[4]} [/]" +
                      $"[purple]CHA: [/][purple4]{abilityValues[5]} [/]")
                .NoBorder());
  
        AnsiConsole.Write(grid);

        //TODO: Add option to create new character 
    }
    public static void DisplayAbilityScores()
    {
        var abilityScores = CharacterClass.AbilityScores;
        int average = AbilityScores.Average(abilityScores);
        var abilityKeys = abilityScores.Keys.ToList();
        var abilityValues = abilityScores.Values.ToList();
        var chart = new BarChart()
            .Label("[bold]Ability Scores[/]")
            .WithMaxValue(5)
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
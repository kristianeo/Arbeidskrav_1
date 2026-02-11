using Arbeidskrav_1.CharacterClasses;
using Arbeidskrav_1.CharacterRepository;
using Arbeidskrav_1.Generators;

using Spectre.Console;

namespace Arbeidskrav_1.UserInterface;

public abstract class CharacterDisplayer
{
    /// <summary>
    /// Displays stats for chosen character.
    /// Uses Modifier() and ConstitutionModifier() for specific class.
    /// </summary>
    /// <param name="character">character to display</param>
    public static void DisplayCharacter(CharacterClass character)
    {
        Console.Clear();
        var prScore = character.GetPrimeRequisite();
        var abilityValues = CharacterClass.AbilityScores.Values.ToList();
        
        AnsiConsole.Status()
            .Start("Calculating scores...", ctx =>
            {
                Thread.Sleep(1500);
  
                ctx.Status("Creating character...");
                Thread.Sleep(2000);
                
            });
  
        AnsiConsole.MarkupLine("[green]Character is created![/]");
        
        var grid = new Grid();
  
        grid.AddColumn(new GridColumn { Width = 20, Alignment = Justify.Right });
        grid.AddColumn(new GridColumn());
  
        grid.AddEmptyRow();
  
        grid.AddRow(new Markup("[bold]Character Name: [/]"), new Markup(character.CharacterName));
        grid.AddRow(new Markup("[bold]Class: [/]"), new Markup(character.ClassName));
        grid.AddRow(new Markup("[bold]Hit Points: [/]"), new Markup(character.HitPoints));
        grid.AddRow(new Markup("[bold]Prime Requisite: [/]"), new Markup(prScore.Item1 + " " + prScore.Item2 + 
                                                                         " (Modifier " + Modifier.Modify(prScore.Item2) + ")"));
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
        
        CharacterSaver.SaveCharacter(character);
        
        CreateOrSearch.Choice();
        
        
    }

}

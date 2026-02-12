using Spectre.Console;

namespace Arbeidskrav_1.CharacterRepository;

public abstract class CharacterDisplayer
{
    /// <summary>
    /// Displays the character stats when the data has been
    /// gathered from the Character repository.
    /// </summary>
    /// <param name="info"></param>
    public static Grid DisplayCharacter(Dictionary<string, string> info)
    {
        var grid = new Grid();
  
        grid.AddColumn(new GridColumn { Width = 20, Alignment = Justify.Right });
        grid.AddColumn(new GridColumn());
  
        grid.AddEmptyRow();
  
        grid.AddRow(new Markup("[bold]Character Name: [/]"), new Markup(info["Character Name: "]));
        grid.AddRow(new Markup("[bold]Class: [/]"), new Markup(info["Class: "]));
        grid.AddRow(new Markup("[bold]Hit Points: [/]"), new Markup(info["Hit Points: "]));
        grid.AddRow(new Markup("[bold]Prime Requisite: [/]"), new Markup(info["Prime Requisite: "] + ": " + info["Prime requisite score: "] + " (Modifier " + info["Modifier: "] + ")"));
        grid.AddRow(new Markup("[bold]XP for level 2: [/]"), new Markup( info["XP for level 2: "]));
        grid.AddRow(
            new Markup("[bold]Ability Scores: [/]"),
            new Panel($"[yellow]STR: [/][yellow4]{info["Strength"]} [/]" +
                      $"[green3]INT: [/][green4]{info["Intelligence"]} [/]" +
                      $"[blue]WIS: [/][blue1]{info["Wisdom"]} [/]")
                .NoBorder());
        grid.AddRow(
            new Markup(""),
            new Panel($"[red]DEX: [/][red3]{info["Dexterity"]}[/] " +
                      $"[darkorange]CON: [/][darkorange3_1]{info["Constitution"]} [/]" +
                      $"[purple]CHA: [/][purple4]{info["Charisma"]} [/]")
                .NoBorder());

        return grid;
    }
}
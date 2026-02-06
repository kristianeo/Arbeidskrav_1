using Spectre.Console;

namespace Arbeidskrav_1.Generators;

public abstract class ChooseClass
{
    /// <summary>
    /// Choose a class from available classes.
    /// If there is only one available class, it chooses for the player.
    /// </summary>
    /// <returns>Chosen character class</returns>
    public static string Choose()
    {
        var available = AvailableClasses.AvailableClass.ToDictionary(kv => $"{kv.Key} {kv.Value}", kv =>  kv.Key);
        if (available.Count == 1)
        {
            var classChoice = available.Keys.First();
            AnsiConsole.MarkupLine($"\n[bold]You have only one available class:[/] [blue]{Markup.Escape(classChoice)} [/]");
            
            return available[classChoice];
        }
        else
        {
            var prompt = new SelectionPrompt<string>()
                .Title("\n[bold]Available classes based on your ability scores: [/]")
                .AddChoices(available.Keys);
            var selected = AnsiConsole.Prompt(prompt);
            var classChoice = available[selected];
            AnsiConsole.MarkupLineInterpolated($"\n[blue]You selected {selected} [/]");
            
            return classChoice;
        }

    }

}
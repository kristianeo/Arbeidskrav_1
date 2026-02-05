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
        var availableClasses = AvailableClasses.AvailableClass; 
        var available = availableClasses.ToDictionary(kv => $"{kv.Key} {kv.Value}", kv =>  kv.Key);
        if (availableClasses.Count == 1)
        {
            var classChoice = availableClasses.Keys.ToList();
            AnsiConsole.MarkupLine($"\n[bold]You have only one available class:[/] [blue]{Markup.Escape(classChoice[0])} [/]");
            
            return classChoice[0];
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
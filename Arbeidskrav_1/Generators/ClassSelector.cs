using Spectre.Console;

namespace Arbeidskrav_1.Generators;

public abstract class ClassSelector
{
    /// <summary>
    /// Player chooses a class from available classes listed in the AvailableClass dict.
    /// If there is only one available class, this is chosen for the player.
    /// </summary>
    /// <returns>Chosen character class</returns>
    public static string ChooseClass()
    {
        var available = AvailableClassesGenerator.AvailableClass.ToDictionary(kv => $"{kv.Key} ({kv.Value.Item1} {kv.Value.Item2})", kv =>  kv.Key);
        if (available.Count == 1)
        {
            var classChoice = available.Keys.First();
            AnsiConsole.MarkupLine($"\n[bold]You have only one available class:[/] [blue]{Markup.Escape(classChoice)}[/]");

            return available[classChoice];
        }
        else
        {
            var prompt = new SelectionPrompt<string>()
                .Title("\n[bold]Available classes based on your ability scores: [/]")
                .AddChoices(available.Keys);
            var selected = AnsiConsole.Prompt(prompt);
            
            AnsiConsole.MarkupLineInterpolated($"\n[bold]You selected [/][blue]{selected} [/]");
         
            return available[selected];
        }

    }

}
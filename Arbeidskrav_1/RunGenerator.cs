using System.Text;
using Arbeidskrav_1.CharacterClasses;
using Arbeidskrav_1.CharacterRepository;
using Arbeidskrav_1.Generators;
using Arbeidskrav_1.UserInterface;
using Spectre.Console;

namespace Arbeidskrav_1;

public abstract class RunGenerator
{
    /// <summary>
    /// Runs the methods needed to create a character
    /// </summary>
    public static void Run()
    {
        AbilityScoresGenerator.GenerateAbilityScores();
        AvailableClassesGenerator.AvailableClasses();
        CharacterClass character = CharacterGenerator.GenerateClass(ClassSelector.ChooseClass());
        UserInterface.CharacterDisplayer.DisplayCharacter(character);
    }
    
    /// <summary>
    /// Shows the starting screen. The player gets to chose if they want to
    /// make a new character or search in the repository.
    /// </summary>
    public static bool WelcomeMessage()
    {
        bool condition = false;
        
        Console.Clear();
        
        var figlet = new FigletText("Welcome")
        {
            Color = Color.Green,
            Justification = Justify.Center
        };
        AnsiConsole.Write(figlet);
        AnsiConsole.Write(new Rule("[green]to the character generator![/]"));
        
        var prompt = new SelectionPrompt<string>()
            .Title("\n[slowblink bold]What would you like to do?[/]")
            .AddChoices("Create character", "Search in character repository");
        var selected = AnsiConsole.Prompt(prompt);
        
        switch (selected)
        {
            case "Create character":
                AnsiConsole.Clear();
                condition = true;
                break;
            case "Search in character repository":
                AnsiConsole.Clear();
                condition = false;
                break;
        }

        return condition;
    }
}

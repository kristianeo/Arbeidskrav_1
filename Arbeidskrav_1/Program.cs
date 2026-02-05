using Arbeidskrav_1.CharacterClasses;
using Arbeidskrav_1.Generators;
using Spectre.Console;

namespace Arbeidskrav_1;

class Program
{
    static void Main(string[] args)
    {
        AnsiConsole.MarkupLine("[green]✓ Build completed successfully[/]");

        AbilityScores.AbilityScoreGenerator();
        AvailableClasses.ClassSelector();
        CharacterClass character = CharacterGenerator.GenerateClass(ChooseClass.Choose());
        character.DisplayCharacter(character.ConstitutionModifier());
        Console.WriteLine(character.ToString());

    }
}
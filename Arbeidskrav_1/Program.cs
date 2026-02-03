using Spectre.Console;

namespace Arbeidskrav_1;

class Program
{
    static void Main(string[] args)
    {
        AnsiConsole.MarkupLine("[green]✓ Build completed successfully[/]");
        CharacterClass.AbilityScoreGenerator();
        CharacterClass.ClassSelection();
        CharacterClass.ChooseClass();
        
        
    }
}
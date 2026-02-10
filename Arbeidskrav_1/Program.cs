using Arbeidskrav_1.CharacterClasses;
using Arbeidskrav_1.Generators;
using Spectre.Console;

namespace Arbeidskrav_1;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine(Directory.GetCurrentDirectory());

        AnsiConsole.MarkupLine("[green]✓ Build completed successfully[/]");

        RunGenerator.Run();
    }
}
using Arbeidskrav_1.CharacterClasses;
using Spectre.Console;

namespace Arbeidskrav_1;

class Program
{
    static void Main(string[] args)
    {
        AnsiConsole.MarkupLine("[green]✓ Build completed successfully[/]");
        Generators.AbilityScoreGenerator();
        Generators.ClassSelector();
        string classChoice = Generators.ChooseClass();
        string characterName = Generators.ChooseCharacterName();
        CharacterClass character = Generators.GenerateClass(classChoice, characterName);
        string hitPoints = character.ConstitutionModifier(character);
        CharacterClass.DisplayCharacter(character, hitPoints);
  
    }
}
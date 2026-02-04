using Spectre.Console;

namespace Arbeidskrav_1;

class Program
{
    static void Main(string[] args)
    {
        AnsiConsole.MarkupLine("[green]✓ Build completed successfully[/]");
        CharacterClass.AbilityScoreGenerator();
        CharacterClass.ClassSelector();
        string classChoice = CharacterClass.ChooseClass();
        string characterName = CharacterClass.ChooseCharacterName();
        CharacterClass character = CharacterClass.GenerateClass(classChoice, characterName);
        string hitPoints = character.ConstitutionModifier(character);
        CharacterClass.DisplayCharacter(character, hitPoints);
        CharacterClass.DisplayCharacter(character, hitPoints);
        
        
        
    }
}
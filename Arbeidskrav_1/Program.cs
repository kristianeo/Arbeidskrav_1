using Arbeidskrav_1.CharacterClasses;
using Arbeidskrav_1.Generators;
using Spectre.Console;

namespace Arbeidskrav_1;

class Program
{
    static void Main(string[] args)
    {
        AnsiConsole.MarkupLine("[green]✓ Build completed successfully[/]");
        AbilityScoreGenerator.AbilityScore();
        AvailableClasses.ClassSelector();
        string classChoice = UserPrompts.ChooseClass();
        string charName = UserPrompts.ChooseCharacterName();
        CharacterClass character = ClassGenerator.GenerateClass(classChoice, charName);
        string hitPoints = character.ConstitutionModifier(character); //TODO: Does it need the (character)?
        CharacterClass.DisplayCharacter(character, hitPoints); //TODO: Change to call from child class not parent class
        
        
        // Generators.ClassSelector();
        // string classChoice = Generators.ChooseClass();
        // string characterName = Generators.ChooseCharacterName();
        // CharacterClass character = Generators.GenerateClass(classChoice, characterName);
        // string hitPoints = character.ConstitutionModifier(character);
        // CharacterClass.DisplayCharacter(character, hitPoints);
        //
    }
}
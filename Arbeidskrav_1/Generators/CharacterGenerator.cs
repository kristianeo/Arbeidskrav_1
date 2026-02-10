using System.Text.Json;
using Arbeidskrav_1.CharacterClasses;
using Spectre.Console;

namespace Arbeidskrav_1.Generators;

public abstract class CharacterGenerator
{
    public static CharacterClass GenerateClass(string? classChoice)
    {
        string charName = GetCharacterName();
        string? choice = classChoice?.ToLower();
        switch (choice)
        {
            case "cleric":
                Cleric cleric = new Cleric(charName, CharacterClass.AbilityScores);
                return cleric;

            case "fighter":
                Fighter fighter = new Fighter(charName, CharacterClass.AbilityScores);
                return fighter;

            case "thief":
                Thief thief = new Thief(charName, CharacterClass.AbilityScores);
                return thief;

            case "magic user":
                MagicUser magicUser = new MagicUser(charName, CharacterClass.AbilityScores);
                return magicUser;
            
            default: //This is never going to run
                Console.WriteLine("Something went wrong.");
                classChoice = ClassSelector.ChooseClass();
                return GenerateClass(classChoice);
        }
    }

    private static string GetCharacterName()
    {
        AskName:
        var charName = AnsiConsole.Ask<string>("[bold]Enter character name: [/]");
        
        if (CharacterExists(charName))
        {
            AnsiConsole.MarkupLine("[red]The character name is already in use.[/]");
            goto AskName;
        }
        
        if (charName.Length is < 3 or > 35)
        {
            AnsiConsole.MarkupLine("[red]Character name must be between 3 and 15 characters long.[/]");
            goto AskName;
        }

        foreach (char item in charName)
        {                        
            if (!char.IsAsciiLetter(item))
            {
                AnsiConsole.MarkupLine("[red]Character name can only contain letters A-Z[/]");
                goto AskName;
            }
        }
        return charName;
    } 
    private static bool CharacterExists(string name)
    {
        const string filePath = 
            @"C:\Users\olsen\OneDrive\Dokumenter\GitHub\Arbeidskrav_1\Arbeidskrav_1\CharacterRepository\CharacterRepo.json";
        string json = File.ReadAllText(filePath);
        
        if (!File.Exists(filePath) || string.IsNullOrWhiteSpace(json))
            return false;

        var characters = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(json);

        return characters != null && characters.Any(c => c.ContainsValue(name));
    }
    
}
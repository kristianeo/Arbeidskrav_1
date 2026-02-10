using Arbeidskrav_1.CharacterClasses;
using Spectre.Console;

namespace Arbeidskrav_1;
using System.Text.Json;

public class CharacterSetter
{
    public static void AddToRepo(Dictionary<string, string> data)
    {
       
        const string filePath = @"C:\Users\olsen\OneDrive\Dokumenter\GitHub\Arbeidskrav_1\Arbeidskrav_1\CharacterRepository\CharacterRepo.json";
        
        List<Dictionary<string, string>> characters = [];

        if (File.Exists(filePath))
        {
            string existingJson = File.ReadAllText(filePath);

            if (!string.IsNullOrWhiteSpace(existingJson))
            {
                characters = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(existingJson)
                             ?? new List<Dictionary<string, string>>();
            }
        }

        characters.Add(data);

        string updatedJson = JsonSerializer.Serialize(
            characters,
            new JsonSerializerOptions { WriteIndented = true }
        );

        File.WriteAllText(filePath, updatedJson);
        AnsiConsole.MarkupLine("[green]Character is saved![/]");
    }

    public static Dictionary<string, string> CreateDictionary(CharacterClass character, string hitPoints)
    {
        Dictionary<string, string> data = [];
        data.Add("Character Name: ", character.CharacterName);
        data.Add("Class: ", character.ClassName);
        data.Add("Hit Points: ", hitPoints);
        foreach (KeyValuePair<string, int> kvp in CharacterClass.AbilityScores)
        {
            data.Add(kvp.Key, $"{kvp.Value}");
        }
        var primeRequisite = character.GetPrimeRequisite();
        data.Add("Prime Requisite: ", primeRequisite.Item1);
        data.Add("Prime requisite score: ", $"{primeRequisite.Item2}");
        data.Add("Modifier: ", Modifier.Modify(primeRequisite.Item2));
        data.Add("XP for level 2: ", $"{character.XpLevel2}");
        return data;
    }

 
}
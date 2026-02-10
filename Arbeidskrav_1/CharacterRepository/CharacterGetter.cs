using System.Text.Json;
using Arbeidskrav_1.CharacterClasses;

namespace Arbeidskrav_1.CharacterRepository;

public class CharacterGetter
{
    public static Dictionary<string, string>? GetCharacter()
    {
        Console.WriteLine("Would you like to search for a character? ");
        string name = Console.ReadLine();
        const string filePath = @"C:\Users\olsen\OneDrive\Dokumenter\GitHub\Arbeidskrav_1\Arbeidskrav_1\CharacterRepo.json";

        if (!File.Exists(filePath))
            return null;

        string json = File.ReadAllText(filePath);

        if (string.IsNullOrWhiteSpace(json))
            return null;

        List<Dictionary<string, string>>? characters =
            JsonSerializer.Deserialize<List<Dictionary<string, string>>>(json);

        if (characters == null)
            return null;

        var info = characters.FirstOrDefault(c => c.ContainsValue(name));
        return info;
    }
    
    
    

}
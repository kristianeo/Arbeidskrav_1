using System.Text.Json;
using Arbeidskrav_1.CharacterRepository;

namespace Arbeidskrav_1.Generators;

public abstract class NameChecker
{
    /// <summary>
    /// Returns true if the given character name already exists in the character repository.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static bool CharacterExists(string name)
    {
        List<Dictionary<string, string>> characters = JsonGetter.GetJson();
        
        return characters.Any(character => 
            character.TryGetValue("Character Name: ",  out var charName)
            && charName.Equals(name, StringComparison.OrdinalIgnoreCase));
    }
}
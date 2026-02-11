using Arbeidskrav_1.CharacterClasses;

namespace Arbeidskrav_1.CharacterRepository;

public abstract class DictionaryCreator
{
    /// <summary>
    /// Creates a dictionary template to save character information to .json
    /// </summary>
    /// <param name="character"></param>
    /// <returns></returns>
    public static Dictionary<string, string> CreateDictionary(CharacterClass character)
    {
        Dictionary<string, string> data = [];
        data.Add("Character Name: ", character.CharacterName);
        data.Add("Class: ", character.ClassName);
        data.Add("Hit Points: ", character.HitPoints);
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
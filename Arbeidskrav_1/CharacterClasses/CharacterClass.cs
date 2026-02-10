using Spectre.Console;

namespace Arbeidskrav_1.CharacterClasses;

public abstract class CharacterClass
{
    private int _xpLevel2;

    private string _className;

    private string _characterName;
    
    private static Dictionary<string, int> _abilityScores = new()
    {
        { "Strength", 0 },
        { "Intelligence", 0 },
        { "Wisdom", 0 },
        { "Dexterity", 0 },
        { "Constitution", 0 },
        { "Charisma", 0 }
    };

    protected CharacterClass(string className, string characterName, int xpLevel2, Dictionary<string, int> abilityScores)
    {
        _xpLevel2 = xpLevel2;
        _className = className;
        _characterName = characterName;
        _abilityScores =  abilityScores;
    }

    public int XpLevel2 => _xpLevel2;

    public string ClassName => _className;

    public string CharacterName => _characterName;
    
    public static Dictionary<string, int> AbilityScores => _abilityScores;

    /// <summary>
    /// Calculates Hit Points for given character class 
    /// </summary>
    /// <returns>*hit points* (xdx +/- x)</returns>
    public abstract string GetHitPoints();
    
    /// <summary>
    /// Displays stats for chosen character
    /// </summary>
    public string DisplayCharacter()
    {  
        var primeRequisite = GetPrimeRequisite();
        string hitPoints = GetHitPoints();
        Console.WriteLine($"\n---CHARACTER CREATED---" +
                          $"\nName: {_characterName}" +
                          $"\nClass: {_className}" +
                          $"\nHit Points: {hitPoints}");
    
        Console.WriteLine("\nAbility Scores:");
        foreach (KeyValuePair<string, int> kvp in _abilityScores)
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
        }

        Console.WriteLine($"\nPrime Requisite: {primeRequisite.Item1} ({primeRequisite.Item2}) - " +
                          $"Modifier: {Modifier.Modify(primeRequisite.Item2)}" +
                          $"\nXP for level 2: {_xpLevel2}");
        return hitPoints;
    }
    
    /// <summary>
    /// Shows Prime requisite for class and calculates modifier using Modifier class
    /// </summary>
    /// <returns>Tuple of prime requisite name and score</returns>
    public abstract Tuple<string, int> GetPrimeRequisite();

    public override string ToString()
    {
        return $"Character Class: {_className} - Character Name: {_characterName}";
    }
}
using Spectre.Console;

namespace Arbeidskrav_1.CharacterClasses;

public abstract class CharacterClass
{
    private int _xpLevel2;

    private string _className;

    private string _characterName;
    
    private string _hitPoints;
    
    private static Dictionary<string, int> _abilityScores = new()
    {
        { "Strength", 0 },
        { "Intelligence", 0 },
        { "Wisdom", 0 },
        { "Dexterity", 0 },
        { "Constitution", 0 },
        { "Charisma", 0 }
    };

    protected CharacterClass(string className, string characterName, int xpLevel2, Dictionary<string, int> abilityScores, string hitPoints)
    {
        _xpLevel2 = xpLevel2;
        _className = className;
        _characterName = characterName;
        _abilityScores =  abilityScores;
        _hitPoints = hitPoints;
    }

    /// <summary>
    /// Public property to display XP needed for level 2
    /// </summary>
    public int XpLevel2 => _xpLevel2;
    /// <summary>
    /// Public property to display class name
    /// </summary>
    public string ClassName => _className;
    /// <summary>
    /// Public property to display chosen character name
    /// </summary>
    public string CharacterName => _characterName;

    public string HitPoints => _hitPoints;

    /// <summary>
    /// Public property to display rolled ability scores
    /// </summary>
    public static Dictionary<string, int> AbilityScores => _abilityScores;

    /// <summary>
    /// Calculates Hit Points for given character class 
    /// </summary>
    /// <returns>*hit points* (xdy +/- z)</returns>
    protected static string GetHitPoints(string dice)
    {
        var constitutionScore = AbilityScores.FirstOrDefault(s => s.Key == "Constitution");
        int modifier = short.Parse(Modifier.Modify(constitutionScore.Value));
        int hitPoints = DiceRoll.RollDice(dice) - modifier;
        if (hitPoints < 1)
        {
            hitPoints = 1;
        }

        return $"{hitPoints} (1d6 {Modifier.Modify(constitutionScore.Value)})";
    }
    
    
    /// <summary>
    /// Displays stats for chosen character //Not used 
    /// </summary>
    public void DisplayCharacter()
    {  
        var primeRequisite = GetPrimeRequisite();
        Console.WriteLine($"\n---CHARACTER CREATED---" +
                          $"\nName: {_characterName}" +
                          $"\nClass: {_className}" +
                          $"\nHit Points: {_hitPoints}");
    
        Console.WriteLine("\nAbility Scores:");
        foreach (KeyValuePair<string, int> kvp in _abilityScores)
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
        }

        Console.WriteLine($"\nPrime Requisite: {primeRequisite.Item1} ({primeRequisite.Item2}) - " +
                          $"Modifier: {Modifier.Modify(primeRequisite.Item2)}" +
                          $"\nXP for level 2: {_xpLevel2}");
    }
    
    /// <summary>
    /// Shows Prime requisite for chosen class and calculates modifier using Modifier class
    /// </summary>
    /// <returns>Tuple of prime requisite name and score</returns>
    public abstract Tuple<string, int> GetPrimeRequisite();

    public override string ToString()
    {
        return $"Character Class: {_className} - Character Name: {_characterName}";
    }
}
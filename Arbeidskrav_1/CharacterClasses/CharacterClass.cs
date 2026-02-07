using Spectre.Console;

namespace Arbeidskrav_1.CharacterClasses;

public abstract class CharacterClass
{
    private int _dice;

    private int _sides;

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


    protected CharacterClass(string className, string characterName, int xpLevel2, int dice, int sides, Dictionary<string, int> abilityScores)
    {
        _dice = dice;
        _sides = sides;
        _xpLevel2 = xpLevel2;
        _className = className;
        _characterName = characterName;
        _abilityScores =  abilityScores;
    }

    public int Dice => _dice;

    public int Sides => _sides;

    public int XpLevel2 => _xpLevel2;

    public string ClassName => _className;

    public string CharacterName => _characterName;
    
    public static Dictionary<string, int> AbilityScores => _abilityScores;


    /// <summary>
    /// Calculates Hit Points for given character class 
    /// </summary>
    /// <returns>Hit Points: x (xdx +/- x)</returns>
    public string GetHitPoints()
    {
        var constitutionScore = AbilityScores.FirstOrDefault(s => s.Key == "Constitution");
        int modifier = short.Parse(Modifier.Modify(constitutionScore.Value));
        int hitPoints = DiceRoll.RollDice(Dice, Sides) - modifier;
        if (hitPoints < 1)
        {
            hitPoints = 1;
        }

        return $"{hitPoints} ({Dice}d{Sides} {Modifier.Modify(constitutionScore.Value)})";
    }
    
    public void DisplayCharacter()
    {  
        var primeRequisite = GetPrimeRequisite();
        Console.WriteLine($"\n---CHARACTER CREATED---" +
                          $"\nName: {_characterName}" +
                          $"\nClass: {_className}" +
                          $"\nHit Points: {GetHitPoints()}");
    
        Console.WriteLine("\nAbility Scores:");
        foreach (KeyValuePair<string, int> kvp in _abilityScores)
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
        }

        Console.WriteLine($"\nPrime Requisite: {primeRequisite.Item1} ({primeRequisite.Item2}) - " +
                          $"Modifier: {Modifier.Modify(primeRequisite.Item2)}" +
                          $"\nXP for level 2: {_xpLevel2}");
    }

    public abstract Tuple<string, int> GetPrimeRequisite();

    public override string ToString()
    {
        return $"Character Class: {_className} - Character Name: {_characterName}";
    }
}
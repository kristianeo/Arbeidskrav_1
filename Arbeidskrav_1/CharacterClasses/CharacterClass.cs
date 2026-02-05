using Spectre.Console;

namespace Arbeidskrav_1.CharacterClasses;

public abstract class CharacterClass
{
    private int _dice;

    private int _sides;

    private int _xpLevel2;

    private string _className;

    private string _primeRequisite;

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


    protected CharacterClass(string className, string primeRequisite, string characterName, int xpLevel2, int dice, int sides, Dictionary<string, int> abilityScores)
    {
        _dice = dice;
        _sides = sides;
        _xpLevel2 = xpLevel2;
        _className = className;
        _primeRequisite = primeRequisite;
        _characterName = characterName;
        _abilityScores =  abilityScores;
    }

    public int Dice => _dice;

    public int Sides => _sides;

    public int XpLevel2 => _xpLevel2;

    public string ClassName => _className;

    public string PrimeRequisite => _primeRequisite;

    public string CharacterName => _characterName;
    
    public static Dictionary<string, int> AbilityScores => _abilityScores;


    /// <summary>
    /// Calculates Hit Points for given character class 
    /// </summary>
    /// <returns>Hit Points: x (xdx +/- x)</returns>
    public virtual string ConstitutionModifier()
    {
        return "";
    }
    public static void DisplayCharacter(CharacterClass character, string hitPoints)
    {  
        var prScore = _abilityScores.FirstOrDefault(s => s.Key == character._primeRequisite);
    
        Console.WriteLine($"\n---CHARACTER CREATED---" +
                          $"\nName: {character._characterName}" +
                          $"\nClass: {character._className}" +
                          $"\n{hitPoints}");
    
        Console.WriteLine("\nAbility Scores:");
        foreach (KeyValuePair<string, int> kvp in _abilityScores)
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
        }

        Console.WriteLine($"\nPrime Requisite: {character._primeRequisite} ({prScore.Value}) - " +
                          $"Modifier: {Modifier.Modify(prScore.Value)}" +
                          $"\nXP for level 2: {character._xpLevel2}");
    }

    public override string ToString()
    {
        return $"Character Class: {_className} - Character Name: {_characterName}";
    }
}
using Spectre.Console;

namespace Arbeidskrav_1;
/// <summary>
/// Creates a template for the derived character classes.
/// </summary>
/// <param name="className">Name of character class</param>
/// <param name="primeRequisite">Specific prime requisite for character class</param>
/// <param name="charName">Chosen character name</param>
/// <param name="xpLevel2">XP needed for level 2</param>
/// <param name="dice">Number of hit dice</param>
/// <param name="sides">Number of sides per die</param>
public abstract class CharacterClass(
    string className,
    string primeRequisite,
    string charName,
    int xpLevel2,
    int dice,
    int sides):Generators
    
{
    protected int _dice = dice;
    
    protected int _sides = sides;
    
    private readonly int _xpLevel2 = xpLevel2;

    private readonly string _className = className;

    private readonly string _primeRequisite = primeRequisite;

    private readonly string _characterName = charName;
    
    public static Dictionary<string, Tuple<string, int>> AvailableClasses = new();
       
    public static Dictionary<string, int> _abilityScores = new()
    {
        { "Strength", 0 },
        { "Intelligence", 0 },
        { "Wisdom", 0 },
        { "Dexterity", 0 },
        { "Constitution", 0 },
        { "Charisma", 0 }
    };
    
    /// <summary>
    /// Calculates Hit Points for given character class 
    /// </summary>
    /// <param name="character"></param>
    /// <returns>Hit Points: x (xdx +/- x)</returns>
    public virtual string ConstitutionModifier(CharacterClass character)
    {
        return "";
    }
    
    /// <summary>
    /// Displays stats for chosen character.
    /// Uses Modifier() and ConstitutionModifier() for specific class.
    /// </summary>
    /// <param name="character">character to display</param>
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
                          $"Modifier: {Modifier(prScore.Value)}" +
                          $"\nXP for level 2: {character._xpLevel2}");
    }



}
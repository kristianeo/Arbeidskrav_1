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
    int sides)
    
{
    protected int _dice = dice;
    
    protected int _sides = sides;
    
    private readonly int _xpLevel2 = xpLevel2;

    private readonly string _className = className;

    private readonly string _primeRequisite = primeRequisite;

    private readonly string _characterName = charName;
    
    static Dictionary<string, Tuple<string, int>> _availableClasses = new();
       
    protected static Dictionary<string, int> _abilityScores = new()
    {
        { "Strength", 0 },
        { "Intelligence", 0 },
        { "Wisdom", 0 },
        { "Dexterity", 0 },
        { "Constitution", 0 },
        { "Charisma", 0 }
    };
    /// <summary>
    /// Generates ability scores for each ability by rolling 3d6.
    /// Minimum value 3, maximum value 18.
    /// Uses Average(), if average is less than or equal to 8
    /// the player gets a chance to reroll.
    /// </summary>
    public static void AbilityScoreGenerator()
    {
        foreach (string value in _abilityScores.Keys)
        {
            _abilityScores[value] += DiceRoll(3, 6);
        }
        
        var abilityKeys = _abilityScores.Keys.ToList();
        var abilityValues = _abilityScores.Values.ToList();
        var chart = new BarChart()
            .Label("[bold underline]Ability Scores[/]")
            .AddItem(abilityKeys[0], abilityValues[0], Color.Green);
            
        AnsiConsole.Write(chart);

        int average = Average();
        if (average <= 8)
        {
            RerollRule();
        }
    }

    private static int Average()
    {
        int total = 0;
        int average;
        foreach (string value in _abilityScores.Keys)
        {
            total += _abilityScores[value];
        }

        average = total / _abilityScores.Count;
        Console.WriteLine($"Average: {average}");
        return average;
    }

    private static void RerollRule()
    { 
        Console.Write("\nYour ability scores are below average. \n" +
                      "Would you like to reroll? Y/N: ");
        char answer = Console.ReadKey().KeyChar;
        switch (answer)
        {
            case 'y':
                AbilityScoreGenerator();
                break;
            case 'n':
                break;
        }
    }
    /// <summary>
    /// Calculates the highest and second-highest ability scores from the ability scores dictionary.
    /// Adds the ability names with their prime requisite and score in an available-classes dictionary.
    /// </summary>
    public static void ClassSelector()
    {
        int highest = -1, secondHighest = -1;

        foreach (string score in _abilityScores.Keys)
        {
            if (_abilityScores[score] > highest)
            {
                secondHighest = highest;
                highest = _abilityScores[score];
            }
            else if (_abilityScores[score] < highest && _abilityScores[score] > secondHighest)
            {
                secondHighest = _abilityScores[score];
            }
        }

        foreach (KeyValuePair<string, int> kvp in _abilityScores)
        {
            if ((kvp.Value == highest && kvp.Key is not "Charisma" and not "Constitution")
                || (kvp.Value == secondHighest && kvp.Key is not "Charisma" and not "Constitution"))
            {
                string available = kvp.Key;
                string chosen = "";
                Tuple<string, int> requisiteScore = new Tuple<string, int>(kvp.Key, kvp.Value);
                switch (available)
                {
                    case "Wisdom":
                        chosen = "Cleric";
                        break;
                    case "Strength":
                        chosen = "Fighter";
                        break;
                    case "Intelligence":
                        chosen = "Magic User";
                        break;
                    case "Dexterity":
                        chosen = "Thief";
                        break;
                }

                _availableClasses.Add(chosen, requisiteScore);
            }
        }
    }
    /// <summary>
    /// Choose a class from available classes.
    /// If there is only one available class, it chooses for the player.
    /// </summary>
    /// <returns>Chosen character class</returns>
    public static string ChooseClass()
    {
        if (_availableClasses.Count == 1)
        {
            var classChoice = _availableClasses.Keys.ToList();
            Console.WriteLine($"\nYou have only one available class: {classChoice[0]} ");
            
            return classChoice[0];
        }
        else
        {
            Console.WriteLine("\nAvailable classes based on your ability scores: ");
            var options = _availableClasses.Keys.ToList();
            var prompt = new SelectionPrompt<string>()
                .AddChoices(options);
            var classChoice = AnsiConsole.Prompt(prompt);

            AnsiConsole.MarkupLine($"You selected [blue]{classChoice}[/]");
           
            return classChoice;
        }

    }
    /// <summary>
    /// The player enters a name.
    /// </summary>
    /// <returns>Character name</returns>
    public static string ChooseCharacterName()
    {
        Console.Write("\nEnter character name: ");
        string charName = Console.ReadLine();
        return charName;
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
    /// <summary>
    /// Generates a new instance of the chosen character class.
    /// </summary>
    /// <param name="classChoice">Chosen class from available classes</param>
    /// <param name="charName">Character name prompted by player</param>
    /// <returns>Instance of selected class</returns>
    public static CharacterClass GenerateClass(string classChoice, string charName)
    {
        string choice = classChoice.ToLower();
        switch (choice)
        {
            case "cleric":
                Cleric cleric = new Cleric(charName);
                cleric.AbilityScore(_abilityScores);
                return cleric;

            case "fighter":
                Fighter fighter = new Fighter(charName);
                fighter.AbilityScore(_abilityScores);
                return fighter;

            case "thief":
                Thief thief = new Thief(charName);
                thief.AbilityScore(_abilityScores);
                return thief;

            case "magic user":
                MagicUser magicUser = new MagicUser(charName);
                magicUser.AbilityScore(_abilityScores);
                return magicUser;

            default:
                return null;
        }
    }
   
    protected static int DiceRoll(int dice, int sides)
    {
        int diceRoll = 0;
        Random random = new Random();
        for (int d = 0; d < dice; d ++)
        {
            diceRoll += random.Next(1, sides+1);
        }
        return diceRoll;
    }

    protected static string Modifier(int num)
    {
        string modifier = num switch
        {
            3 => "-3",
            4 or 5 => "-2",
            6 or 7 or 8 => "-1",
            9 or 10 or 11 or 12 => "+0",
            13 or 14 or 15 => "+1",
            16 or 17 => "+2",
            18 => "+3",
            _ => "0"
        };

        return modifier;
    }
    
    /// <summary>
    /// Calculates Hit Points for given character class 
    /// </summary>
    /// <param name="character"></param>
    /// <returns>Hit Points: x (xdx +/- x)</returns>
    public virtual string ConstitutionModifier(CharacterClass character)
    {
        return "string";
    }



}
using Spectre.Console;

namespace Arbeidskrav_1;

public abstract class CharacterClass(
    string className,
    string primeRequisite,
    string charName,
    int xpLevel2,
    int dice,
    int sides)
    : DiceRoll
{
    private int _dice = dice;
    
    private int _sides = sides;
    
    private int _xpLevel2 = xpLevel2;

    protected string ClassName = className;

    protected string PrimeRequisite = primeRequisite;

    protected string CharacterName = charName;

    static Dictionary<string, Tuple<string, int>> availableClasses = new();
    
    
    public static void AbilityScoreGenerator()
    {
        foreach (string value in _abilityGenerator.Keys)
        {
            _abilityGenerator[value] += Diceroll(3, 6);
        }

        foreach (KeyValuePair<string, int> kvp in _abilityGenerator)
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
        }

        RerollRule();
    }
    
    private static Dictionary<string, int> _abilityGenerator = new()
    {
        { "Strength", 0 },
        { "Intelligence", 0 },
        { "Wisdom", 0 },
        { "Dexterity", 0 },
        { "Constitution", 0 },
        { "Charisma", 0 }
    };


    private static void RerollRule()
    { 
        int total = 0;
        int average;
        foreach (string value in _abilityGenerator.Keys)
        {
            total += _abilityGenerator[value];
        }

        average = total / _abilityGenerator.Count;
        Console.WriteLine($"Average: {average}");

        if (average <= 8)
        {
            Console.Write("Your ability scores are below average. \n" +
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
    }
    

    public static void ClassSelection()
    {
        int largest = -1, secondLargest = -1;

        foreach (string score in _abilityGenerator.Keys)
        {
            if (_abilityGenerator[score] > largest)
            {
                secondLargest = largest;
                largest = _abilityGenerator[score];
            }
            else if (_abilityGenerator[score] < largest && _abilityGenerator[score] > secondLargest)
            {
                secondLargest = _abilityGenerator[score];
            }
        }

        foreach (KeyValuePair<string, int> kvp in _abilityGenerator)
        {
            if ((kvp.Value == largest && kvp.Key is not "Charisma" and not "Constitution")
                || (kvp.Value == secondLargest && kvp.Key is not "Charisma" and not "Constitution"))
            {
                string requisite = kvp.Key;
                string chosen = "";
                Tuple<string, int> requisit = new Tuple<string, int>(kvp.Key, kvp.Value);
                switch (requisite)
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

                availableClasses.Add(chosen, requisit);
            }
        }
    }

    public static void ChooseClass()
    {
        if (availableClasses.Count == 1)
        {
            var classChoice = availableClasses.Keys.ToList();
            Console.WriteLine($"\nYou have only one available class: {classChoice[0]} ");
            Console.Write("\nEnter character name: ");
            string charName = Console.ReadLine();

            CharacterClass character = GenerateClass(classChoice[0], charName);
            DisplayCharacter(character);
        }
        else
        {
            Console.WriteLine("\nAvailable classes based on your ability scores: ");
            var options = availableClasses.Keys.ToList();
            var prompt = new SelectionPrompt<string>()
                .AddChoices(options);
            var classChoice = AnsiConsole.Prompt(prompt);

            AnsiConsole.MarkupLine($"You selected [blue]{classChoice}[/]");
            Console.Write("\nEnter character name: ");
            string charName = Console.ReadLine();
            CharacterClass character = GenerateClass(classChoice, charName);
            DisplayCharacter(character);
        }

    }

    public static void DisplayCharacter(CharacterClass character)
    {
        var prScore = _abilityGenerator.FirstOrDefault(s => s.Key == character.PrimeRequisite);
        
        Console.WriteLine($"\n---CHARACTER CREATED---" +
                          $"\nName: {character.CharacterName}" +
                          $"\nClass: {character.ClassName}" +
                          $"\n{ConstitutionModifier(character)}");
        
        Console.WriteLine("\nAbility Scores:");
        foreach (KeyValuePair<string, int> kvp in _abilityGenerator)
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
        }

        Console.WriteLine($"\nPrime Requisite: {character.PrimeRequisite} ({prScore.Value}) - " +
                          $"Modifier: {Modifier(prScore.Value)}" +
                          $"\nXP for level 2: {character._xpLevel2}");
    }

    public static CharacterClass GenerateClass(string classChoice, string charName)
    {
        string choice = classChoice.ToLower();
        switch (choice)
        {
            case "cleric":
                Cleric cleric = new Cleric(charName);
                return cleric;

            case "fighter":
                Fighter fighter = new Fighter(charName);
                return fighter;

            case "thief":
                Thief thief = new Thief(charName);
                return thief;

            case "magic user":
                MagicUser magicUser = new MagicUser(charName);
                return magicUser;

            default:
                return null;
        }
    }

    public static string Modifier(int num)
    {
        string modifier = "0";
        switch (num)
        {
            case 3:
                modifier = "-3";
                break;
            case 4:
            case 5:
                modifier = "-2";
                break;
            case 6:
            case 7:
            case 8:
                modifier = "-1";
                break;
            case 9:
            case 10:
            case 11:
            case 12:
                modifier = "0";
                break;
            case 13:
            case 14:
            case 15:
                modifier = "+1";
                break;
            case 16:
            case 17:
                modifier = "+2";
                break;
            case 18:
                modifier = "+3";
                break;
        }

        return modifier;
    }

    public static string ConstitutionModifier(CharacterClass character)
    {
        var constitutionScore = _abilityGenerator.FirstOrDefault(s => s.Key == "Constitution");
        int modifier = Int16.Parse(Modifier(constitutionScore.Value));
        int hitPoints = Diceroll(character._dice, character._sides) - modifier;
        if (hitPoints < 1)
        {
            hitPoints = 1;
        }
        
        return $"Hit Points: {hitPoints} ({character._dice}d{character._sides} + {modifier})";
    }

}
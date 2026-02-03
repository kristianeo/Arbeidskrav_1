using Spectre.Console;

namespace Arbeidskrav_1;

public abstract class CharacterClass:DiceRoll
{
    private int _average = RerollRule();

    protected string ClassName;
    
    protected string PrimeRequisite;
    
    protected string CharacterName;

    protected CharacterClass(string className, string primeRequisite,string charName)
    {
        ClassName = className;
        PrimeRequisite =  primeRequisite; 
        CharacterName = charName;
    }

    static Dictionary < string, Tuple<string, int>> availableClasses = new ();

   public static void AbilityScoreGenerator()
    {
        foreach (string value in _abilityGenerator.Keys)
        {
            _abilityGenerator[value] += Diceroll(3,6);
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

    private static int RerollRule()
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

        return average;
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
                availableClasses.Add(chosen,requisit);
            }
        }
    }

    public static void ChooseClass()
    {
        if (availableClasses.Count == 1)
        {
            var classChoice = availableClasses.Keys.ToList();
            Console.WriteLine($"You have only one available class: {classChoice[0]} ");
            Console.Write("\nEnter character name: ");
            string charName = Console.ReadLine();

            CharacterClass character = GenerateClass(classChoice[0], charName);
            DisplayCharacter(character);

        }
        else
        {
            Console.WriteLine("\nAvailable classes based on your availability scores: ");
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
        Console.WriteLine($"\n---CHARACTER CREATED---" +
                          $"\nName: {character.CharacterName}" +
                          $"\nClass: {character.ClassName}");
        Console.WriteLine("\nAbility Scores:");
        foreach (KeyValuePair<string, int> kvp in _abilityGenerator)
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
        }
    }

    public static CharacterClass GenerateClass(string classChoice, string charName)
    {
        string choice = classChoice.ToLower();
        switch (choice)
        {
            case "cleric":
                Cleric cleric = new Cleric(charName);
            return cleric;
            
            case  "fighter":
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

    public static int Modifier(int num)
    {
        int modifier = 0;
        switch (num)
        {
            case 3:
                modifier = -3;
                break;
            case 4: case 5:
                modifier = -2;
                break;
            case 6: case 7: case 8:
                modifier = -1;
                break;
            case 9: case 10: case 11: case 12:
                modifier = 0;
                break;
            case 13: case 14: case 15:
                modifier = 1;
                break;
            case 16: case 17:
                modifier = 2;
                break;
            case 18:
                modifier = 3;
                break;
        }
        return modifier;
    }
}
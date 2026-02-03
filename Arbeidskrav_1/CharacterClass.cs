using Spectre.Console;

namespace Arbeidskrav_1;

public abstract class CharacterClass:DiceRoll
{
    private int _average = RerollRule();

    protected string PrimeRequisite;

    protected string Name;

    protected CharacterClass(string name, string primeRequisite)
    {
        Name = name;
        PrimeRequisite =  primeRequisite;   
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
                string chosen = null;
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
        Console.WriteLine("\nAvailable classes based on your availability scores: ");
        if (availableClasses.Count == 1)
        {
            var className = availableClasses.Keys.ToList();
            string classname = className[0];
            Console.WriteLine($"You have only one available class: {classname} ");
            Console.Write("\nEnter character name: ");
            string name = Console.ReadLine();
            
            DisplayCharacter(name, classname);
            GenerateClass(classname);

        }
        else
        {
            var options = availableClasses.Keys.ToList();
            var prompt = new SelectionPrompt<string>()
                .AddChoices(options);

            var selectedOption = AnsiConsole.Prompt(prompt);

            AnsiConsole.MarkupLine($"You selected [blue]{selectedOption}[/]");
            Console.Write("\nEnter character name: ");
            string name = Console.ReadLine();
            GenerateClass(selectedOption);
            DisplayCharacter(name, selectedOption);
        }

    }

    public static void DisplayCharacter(string name, string classname)
    {
        Console.WriteLine($"*Character created*" +
                          $"\nName: {name}" +
                          $"\nClass: {classname}");
    }

    public static CharacterClass GenerateClass(string classChoice)
    {
        if (classChoice.ToLower() == "cleric")
        {
            Cleric cleric = new Cleric
            {
                ClericAbilities = _abilityGenerator
            };
            return cleric;
        }

        if (classChoice.ToLower() == "fighter")
        {
            Fighter fighter = new Fighter
            {
                FighterAbilities = _abilityGenerator
            };
            return fighter;
        }

        if (classChoice.ToLower() == "thief")
        {
            Thief thief = new Thief
            {
                ThiefAbilities = _abilityGenerator
            };
            return thief;
        }

        if (classChoice.ToLower() == "magic user")
        {
            MagicUser magicUser = new MagicUser
            {
                MagicAbilities = _abilityGenerator
            };
            return magicUser;
        }
        return null;
    }
}
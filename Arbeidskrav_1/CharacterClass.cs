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
        foreach (string value in AbilityGenerator.Keys)
        {
            AbilityGenerator[value] += Diceroll(3,6);
        }

        foreach (KeyValuePair<string, int> kvp in AbilityGenerator)
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
        }

        RerollRule();

    }

    private static Dictionary<string, int> AbilityGenerator = new()
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
        foreach (string value in AbilityGenerator.Keys)
        {
            total += AbilityGenerator[value];
        }

        average = total / AbilityGenerator.Count;
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

        foreach (string score in AbilityGenerator.Keys)
        {
            if (AbilityGenerator[score] > largest)
            {
                secondLargest = largest;
                largest = AbilityGenerator[score];
            }
            else if (AbilityGenerator[score] < largest && AbilityGenerator[score] > secondLargest)
            {
                secondLargest = AbilityGenerator[score];
            }
        }

        foreach (KeyValuePair<string, int> kvp in AbilityGenerator)
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
                        Cleric cleric = new Cleric();
                        Cleric.ClericAbilities = AbilityGenerator;
                        chosen = cleric.Name;
                        break;
                    case "Strength":
                        Fighter fighter = new Fighter();
                        chosen = fighter.Name;
                        break;
                    case "Intelligence": 
                        MagicUser magicUser = new MagicUser();
                        chosen = magicUser.Name;
                        break;
                    case "Dexterity": 
                        Thief thief = new Thief();
                        chosen = thief.Name; 
                        break;
                }
                availableClasses.Add(chosen,requisit);
            }
        }
    }

    public static void ChooseClass()
    {
        Console.WriteLine("\nAvailable classes based on your availability scores: ");
        
        int count = 1;
        foreach (KeyValuePair<string, Tuple<string, int>> className in availableClasses)
        {
            Console.WriteLine($"{count}. {className.Key} (Prime: {className.Value.Item1} - {className.Value.Item2})");
            count++;
        }

        if (availableClasses.Count == 1)
        {
            Console.WriteLine("You have only one available class. ");
            Console.Write("\nEnter character name: ");
            string name = Console.ReadLine();
            string className = availableClasses.Keys.ElementAt(0);
            DisplayCharacter(name, className);
            
        }
        else
        {
            Console.Write($"\nChoose class (1-{count-1}): ");
            GenerateCharacter();
        }
    }

    public static void GenerateCharacter()
    {
        int choice = Console.ReadKey().KeyChar;

        Console.Write("\nEnter character name: ");
        string name = Console.ReadLine();
        // if (!string.IsNullOrEmpty(name))
        // {
        //     Console.Write("You must enter a name, try again: ");
        //     name = Console.ReadLine();
        // }
        string className = "";
        
        switch (choice)
        {
            case 1:
                className = availableClasses.Keys.ElementAt(0);
                DisplayCharacter(name, className);
                break;
            case 2:
                className = availableClasses.Keys.ElementAt(1);
                DisplayCharacter(name, className);
                break;
            case 3:
                className = availableClasses.Keys.ElementAt(2);
                DisplayCharacter(name, className);
                break;
            case 4:
                className = availableClasses.Keys.ElementAt(3);
                DisplayCharacter(name, className);
                break;
        }
    }

    public static void DisplayCharacter(string name, string classname)
    {
        Console.WriteLine($"*Character created*" +
                          $"\nName: {name}" +
                          $"\nClass: {classname}");
    }
}
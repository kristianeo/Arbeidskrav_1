namespace Arbeidskrav_1;

public abstract class CharacterClass
{
    private int _average = RerollRule();

    protected string PrimeRequisite;

    protected CharacterClass(string primeRequisite)
    {
        PrimeRequisite =  primeRequisite;   
    }
    
    static Dictionary < string, Tuple<string, int>> availableClasses = new ();

   public static void AbilityScoreGenerator()
    {
        foreach (string value in AbilityGenerator.Keys)
        {
            AbilityGenerator[value] += Generators.DiceRoll(3, 6);
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
                    case "Wisdom": chosen = "Cleric"; break;
                    case "Strength": chosen = "Fighter"; break;
                    case "Intelligence": chosen = "Magic User"; break;
                    case "Dexterity": chosen = "Thief"; break;
                    default: break;
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

        Console.Write($"\nWhich class do you chose? (1-{count-1})");
        
        string choice = Console.ReadKey().KeyChar.ToString();
    }
}
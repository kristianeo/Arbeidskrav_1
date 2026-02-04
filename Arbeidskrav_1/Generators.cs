using System.Reflection.Metadata.Ecma335;
using Spectre.Console;

namespace Arbeidskrav_1;

public class Generators
{
    protected static int Average(Dictionary<string, int> dictionary)
    {
        int total = 0;
        int average;
        foreach (string value in dictionary.Keys)
        {
            total += dictionary[value];
        }

        average = total / dictionary.Count;
        return average;
    }
    /// <summary>
    /// Generates ability scores for each ability by rolling 3d6.
    /// Minimum value 3, maximum value 18.
    /// Uses Average(), if average is less than or equal to 8
    /// the player gets a chance to reroll.
    /// </summary>
    public static void AbilityScoreGenerator(Dictionary<string, int> dictionary)
    {
        TryAgain:
        foreach (string value in dictionary.Keys)
        {
            dictionary[value] += DiceRoll(3, 6);
        }
        
        int average = Average(dictionary);
        if (average <= 8)
        {
            Console.Write("\nYour ability scores are below average. \n" +
                          "Would you like to reroll? Y/N: ");
            char answer = Console.ReadKey().KeyChar;
            switch (answer)
            {
                case 'y':
                    goto TryAgain;
                case 'n':
                    break;
            }
        }
    }
    
    /// <summary>
    /// Calculates the highest and second-highest ability scores from the ability scores dictionary.
    /// Adds the ability names with their prime requisite and score in an available-classes dictionary.
    /// </summary>
    public static void ClassSelector(Dictionary<string, int> dictionary)
    {
        int highest = -1, secondHighest = -1;

        foreach (string score in dictionary.Keys)
        {
            if (dictionary[score] > highest)
            {
                secondHighest = highest;
                highest = dictionary[score];
            }
            else if (dictionary[score] < highest && dictionary[score] > secondHighest)
            {
                secondHighest = dictionary[score];
            }
        }

        foreach (KeyValuePair<string, int> kvp in dictionary)
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

                CharacterClass.AvailableClasses.Add(chosen, requisiteScore);
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
        var availableClasses = CharacterClass.AvailableClasses;
        if (availableClasses.Count == 1)
        {
            var classChoice = availableClasses.Keys.ToList();
            AnsiConsole.MarkupLine($"\n[bold]You have only one available class:[/] [blue]{classChoice[0]}[/] ");
            
            return classChoice[0];
        }
        else
        {
            var available = availableClasses.ToDictionary(kv => $"{kv.Key} {kv.Value}", kv =>  kv.Key);
            var prompt = new SelectionPrompt<string>()
                .Title("\n[bold]Available classes based on your ability scores: [/]")
                .AddChoices(available.Keys);
            var selected = AnsiConsole.Prompt(prompt);
            var classChoice = available[selected];
            AnsiConsole.MarkupLine($"\n[blue]You selected {classChoice}[/]");
            
            return classChoice;
        }

    }

    /// <summary>
    /// The player enters a name.
    /// </summary>
    /// <returns>Character name</returns>
    public static string ChooseCharacterName()
    {
        var charName = AnsiConsole.Ask<string>("[blue]Enter character name: [/]");
        
        return charName;
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
                cleric.AbilityScore(CharacterClass._abilityScores);
                return cleric;

            case "fighter":
                Fighter fighter = new Fighter(charName);
                fighter.AbilityScore(CharacterClass._abilityScores);
                return fighter;

            case "thief":
                Thief thief = new Thief(charName);
                thief.AbilityScore(CharacterClass._abilityScores);
                return thief;

            case "magic user":
                MagicUser magicUser = new MagicUser(charName);
                magicUser.AbilityScore(CharacterClass._abilityScores);
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
}
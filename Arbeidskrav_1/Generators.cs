using System.Reflection.Metadata.Ecma335;
using Arbeidskrav_1.CharacterClasses;
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
    public static void AbilityScoreGenerator()
    {
        TryAgain:
        foreach (string value in CharacterClass._abilityScores.Keys)
        {
            CharacterClass._abilityScores[value] = 0;
            CharacterClass._abilityScores[value] += DiceRoll(3, 6);
        }
        
        DisplayAbilityScores();
        
        int average = Average(CharacterClass._abilityScores);
        if (average <= 8)
        {
            Console.WriteLine();
            if (AnsiConsole.Confirm("Your ability scores are below average. " +
                                    "\nWould you like to reroll?"))
            {
                Console.Clear();
                AnsiConsole.Status()
                    .Spinner(Spinner.Known.Arrow)
                    .Start("Rerolling...", ctx =>
                    {
                        Thread.Sleep(1500);
                    });
  
                AnsiConsole.MarkupLine("[green]Rerolled!![/]");
                goto TryAgain;
            }
            
        }
    }

    /// <summary>
    /// Calculates the highest and second-highest ability scores from the ability scores dictionary.
    /// Adds the ability names with their prime requisite and score in an available-classes dictionary.
    /// </summary>
    public static void ClassSelector()
    {
        int highest = -1, secondHighest = -1;

        foreach (string score in CharacterClass._abilityScores.Keys)
        {
            if (CharacterClass._abilityScores[score] > highest)
            {
                secondHighest = highest;
                highest = CharacterClass._abilityScores[score];
            }
            else if (CharacterClass._abilityScores[score] < highest && CharacterClass._abilityScores[score] > secondHighest)
            {
                secondHighest = CharacterClass._abilityScores[score];
            }
        }

        foreach (KeyValuePair<string, int> kvp in CharacterClass._abilityScores)
        {
            if ((kvp.Value == highest && kvp.Key is not "Charisma" and not "Constitution") || 
                (kvp.Value == secondHighest && kvp.Key is not "Charisma" and not "Constitution"))
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

        if (CharacterClass.AvailableClasses.Count == 0)
        {
            AnsiConsole.WriteLine("You have no available classes based on your ability scores.");
            AnsiConsole.Status()
                .Start("Rerolling...", ctx => { Thread.Sleep(1500); });

            AnsiConsole.MarkupLine("[green]Rerolled!![/]");
            AbilityScoreGenerator(); //runs these methods again to get valid data 
            ClassSelector();
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
        var available = availableClasses.ToDictionary(kv => $"{kv.Key} {kv.Value}", kv =>  kv.Key);
        if (availableClasses.Count == 1)
        {
            var classChoice = availableClasses.Keys.ToList();
            AnsiConsole.MarkupLine($"\n[bold]You have only one available class:[/] [blue]{Markup.Escape(classChoice[0])} [/]");
            
            return classChoice[0];
        }
        else
        {
            var prompt = new SelectionPrompt<string>()
                .Title("\n[bold]Available classes based on your ability scores: [/]")
                .AddChoices(available.Keys);
            var selected = AnsiConsole.Prompt(prompt);
            var classChoice = available[selected];
            AnsiConsole.MarkupLineInterpolated($"\n[blue]You selected {selected} [/]");
            
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
    
    public static void DisplayAbilityScores()
    {
        var abilityScores = CharacterClass._abilityScores;
        int average = Average(abilityScores);
        var abilityKeys = abilityScores.Keys.ToList();
        var abilityValues = abilityScores.Values.ToList();
        var chart = new BarChart()
            .Label("[bold]Ability Scores[/]")
            .WithMaxValue(12)
            .AddItem(abilityKeys[0], abilityValues[0], Color.Red)
            .AddItem(abilityKeys[1], abilityValues[1], Color.Blue)
            .AddItem(abilityKeys[2], abilityValues[2], Color.Green)
            .AddItem(abilityKeys[3], abilityValues[3], Color.Yellow)
            .AddItem(abilityKeys[4], abilityValues[4], Color.Orange1)
            .AddItem(abilityKeys[5], abilityValues[5], Color.Purple)
            .AddItem("Average", average, Color.Red3);
            
        AnsiConsole.Write(chart);
    }
}
using Arbeidskrav_1.CharacterClasses;
using Spectre.Console;

namespace Arbeidskrav_1.Generators;

public class AvailableClasses
{
    public static readonly Dictionary<string, Tuple<string, int>> AvailableClass = new();
    private static List<int> _highestScores = [];
    
    /// <summary>
    /// Calculates the highest and second-highest ability scores from the ability scores dictionary.
    /// Adds the ability names with their prime requisite and score in an available-classes dictionary.
    /// </summary>
    public static void ClassSelector()
    {
        AvailableClass.Clear();
        foreach (KeyValuePair<string, int> kvp in CharacterClass.AbilityScores)
        {
            if (kvp.Key is "Charisma" or "Constitution")
            {
                break;
            }
            if (kvp.Value == _highestScores.Max() || kvp.Value == _highestScores.Min())
            {
                string available = kvp.Key;
                string availableClass = "";
                Tuple<string, int> requisiteScore = new Tuple<string, int>(kvp.Key, kvp.Value);
                switch (available)
                {
                    case "Wisdom":
                        availableClass = "Cleric";
                        break;
                    case "Strength":
                        availableClass = "Fighter";
                        break;
                    case "Intelligence":
                        availableClass = "Magic User";
                        break;
                    case "Dexterity":
                        availableClass = "Thief";
                        break;
                }

                AvailableClass.Add(availableClass, requisiteScore);
            }
        }
    }

    public static void CalculateHighestScores()
    {
        _highestScores.Clear(); //Clears list when creating new character
        int highest = 0, secondHighest = 0;

        foreach (var kvp in CharacterClass.AbilityScores)
        {
            if (kvp.Value > highest)
            {
                secondHighest = highest;
                highest = kvp.Value;
            }
            else if (kvp.Value < highest && kvp.Value > secondHighest)
            {
                secondHighest = kvp.Value;
            }
        }
        _highestScores.Add(highest);
        _highestScores.Add(secondHighest);
    }

    public static void NoAvailableClassesCheck()
    {
        if (AvailableClass.Count != 0) return;
        AnsiConsole.WriteLine("You have no available classes based on your ability scores.");
        AnsiConsole.Status()
            .Start("Rerolling...", ctx => { Thread.Sleep(1500); });

        AnsiConsole.MarkupLine("[green]Rerolled!![/]");
        RunGenerator.Run();
    }
}
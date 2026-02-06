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
        foreach (KeyValuePair<string, int> kvp in CharacterClass.AbilityScores)
        {
            if (kvp.Key is "Charisma" or "Constitution")
            {
                break;
            }
            if (kvp.Value == _highestScores.Max() || kvp.Value == _highestScores.Min())
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

                AvailableClass.Add(chosen, requisiteScore);
            }
        }
    }

    public static void CalculateHighestScores()
    {
        int highest = 0, secondHighest = 0;

        foreach (var kvp in CharacterClass.AbilityScores.TakeWhile(kvp => kvp.Key is not ("Charisma" or "Constitution")))
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
        if (_highestScores.Count != 0) return;
        AnsiConsole.WriteLine("You have no available classes based on your ability scores.");
        AnsiConsole.Status()
            .Start("Rerolling...", ctx => { Thread.Sleep(1500); });

        AnsiConsole.MarkupLine("[green]Rerolled!![/]");
        AbilityScores.AbilityScoreGenerator();
    }

}
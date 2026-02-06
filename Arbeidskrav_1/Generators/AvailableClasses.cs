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
    public static void ClassSelector() //TODO: fix it so this code can be accessed earlier for abilityscore
    {
        

        foreach (KeyValuePair<string, int> kvp in CharacterClass.AbilityScores)
        {
            if ((kvp.Value == _highestScores.Max() && kvp.Key is not "Charisma" and not "Constitution") || 
                (kvp.Value == _highestScores.Min() && kvp.Key is not "Charisma" and not "Constitution"))
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

        if (AvailableClass.Count == 0)
        {
            AnsiConsole.WriteLine("You have no available classes based on your ability scores.");
            AnsiConsole.Status()
                .Start("Rerolling...", ctx => { Thread.Sleep(1500); });

            AnsiConsole.MarkupLine("[green]Rerolled!![/]");
        }
    }

    public static void CalculateHighestScores()
    {
        int highest = -1, secondHighest = -1;

        foreach (string score in CharacterClass.AbilityScores.Keys)
        {
            if (CharacterClass.AbilityScores[score] > highest)
            {
                secondHighest = highest;
                highest = CharacterClass.AbilityScores[score];
            }
            else if (CharacterClass.AbilityScores[score] < highest && CharacterClass.AbilityScores[score] > secondHighest)
            {
                secondHighest = CharacterClass.AbilityScores[score];
            }
        }
        _highestScores.Add(highest);
        _highestScores.Add(secondHighest);
    }

}
using Arbeidskrav_1.CharacterClasses;
using Spectre.Console;

namespace Arbeidskrav_1.Generators;

public static class AvailableClassesGenerator
{
    public static readonly Dictionary<string, (string, int)> AvailableClass = new();
    private static List<int> _highestScores = [];
    
    /// <summary>
    /// Uses CalculateHighScores() to find the two highest ability scores.
    /// Adds the valid class names and their prime requisite and score in an available-classes dictionary.
    /// Checks if there is no valid class using NoAvailableClassesCheck().
    /// </summary>
    public static void AvailableClasses()
    {
        AvailableClass.Clear();
        
        List<int> highScores = CalculateHighestScores();
        
        foreach (KeyValuePair<string, int> kvp in CharacterClass.AbilityScores)
        {
            if (kvp.Key is "Charisma" or "Constitution") continue;

            if (kvp.Value != highScores.Max() && kvp.Value != highScores.Min()) continue;
            
            string available = kvp.Key;
            var availableClass = CharacterClass.ClassInfo.FirstOrDefault(s => s.Key == available);
  
            AvailableClass.Add(availableClass.Value.Item1, (kvp.Key, kvp.Value));
            
        }
        NoAvailableClassesCheck();
    }

    private static List<int> CalculateHighestScores()
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
        return _highestScores;
    }

    private static void NoAvailableClassesCheck()
    {
        if (AvailableClass.Count != 0) return;
        AnsiConsole.WriteLine("You have no available classes based on your ability scores.");
        RunGenerator.Run(); //Starts rolling new ability scores
    }
}
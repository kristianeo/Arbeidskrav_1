using System.Runtime.CompilerServices;

namespace Arbeidskrav_1;

public class Fighter(string charName) :CharacterClass("Fighter", "Strength", charName, 2000, 1, 8)
{
    public override string ConstitutionModifier(CharacterClass character)
    {
        var constitutionScore = AbilityScores.FirstOrDefault(s => s.Key == "Constitution");
        int modifier = Int16.Parse(Modifier(constitutionScore.Value));
        int hitPoints = DiceRoll(_dice, _sides) - modifier;
        if (hitPoints < 1)
        {
            hitPoints = 1;
        }
        
        return $"Hit Points: {hitPoints} ({_dice}d{_sides} {modifier})";
    }

    public Dictionary<string, int> AbilityScore(Dictionary<string, int> dict)
    {
        AbilityScores = dict;
        return AbilityScores;
    }
    
    private Dictionary<string, int> AbilityScores = new();


    
    
}
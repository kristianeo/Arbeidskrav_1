namespace Arbeidskrav_1;

public class Cleric(string charName) :CharacterClass("Cleric", "Wisdom", charName, 1500, 1, 6)
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
    public Dictionary<string, int> AbilityScores = new Dictionary<string, int>();

}
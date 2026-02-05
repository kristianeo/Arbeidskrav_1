namespace Arbeidskrav_1.CharacterClasses;

public class Cleric(string charName, Dictionary<string, int> abilityScores) :
    CharacterClass("Cleric", "Wisdom", charName, 1500, 1, 6, abilityScores)
{
    public override string ConstitutionModifier(CharacterClass character)
    {
        var constitutionScore = AbilityScores.FirstOrDefault(s => s.Key == "Constitution");
        string mod = Modifier.Modify(constitutionScore.Value);
        int modifier = Int16.Parse(mod);
        int hitPoints = DiceRoll.RollDice(Dice, Sides) - modifier;
        if (hitPoints < 1)
        {
            hitPoints = 1;
        }
        return $"{hitPoints} ({Dice}d{Sides} {mod})";
    }

}
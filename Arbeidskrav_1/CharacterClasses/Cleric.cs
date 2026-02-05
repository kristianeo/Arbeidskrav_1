namespace Arbeidskrav_1.CharacterClasses;

public class Cleric(string charName, Dictionary<string, int> abilityScores) :
    CharacterClass("Cleric", "Wisdom", charName, 1500, 1, 6, abilityScores)
{
    public override string ConstitutionModifier()
    {
        var constitutionScore = AbilityScores.FirstOrDefault(s => s.Key == "Constitution");
        int modifier = short.Parse(Modifier.Modify(constitutionScore.Value));
        int hitPoints = DiceRoll.RollDice(Dice, Sides) - modifier;
        if (hitPoints < 1)
        {
            hitPoints = 1;
        }
        return $"{hitPoints} ({Dice}d{Sides} {modifier})";
    }

}
namespace Arbeidskrav_1.CharacterClasses;

public class Cleric(string charName, Dictionary<string, int> abilityScores) :
    CharacterClass("Cleric", charName, 1500, abilityScores)
{
    public override string GetHitPoints()
    {
        var constitutionScore = AbilityScores.FirstOrDefault(s => s.Key == "Constitution");
        int modifier = short.Parse(Modifier.Modify(constitutionScore.Value));
        int hitPoints = DiceRoll.RollDice(1, 6) - modifier;
        if (hitPoints < 1)
        {
            hitPoints = 1;
        }

        return $"{hitPoints} (1d6 {Modifier.Modify(constitutionScore.Value)})";
    }

    public override Tuple<string, int> GetPrimeRequisite()
    {
        string primerequisite = "Wisdom";
        var prScore = AbilityScores.FirstOrDefault(s => s.Key == primerequisite);
        int primeRequisiteScore = prScore.Value;
        return Tuple.Create(primerequisite, primeRequisiteScore);
    }
}
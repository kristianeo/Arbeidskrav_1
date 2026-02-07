namespace Arbeidskrav_1.CharacterClasses;

public class Fighter(string charName, Dictionary<string, int> abilityScores) :
    CharacterClass("Fighter", charName, 2000, abilityScores)
{
    public override string GetHitPoints()
    {
        var constitutionScore = AbilityScores.FirstOrDefault(s => s.Key == "Constitution");
        int modifier = short.Parse(Modifier.Modify(constitutionScore.Value));
        int hitPoints = DiceRoll.RollDice(1, 8) - modifier;
        if (hitPoints < 1)
        {
            hitPoints = 1;
        }

        return $"{hitPoints} (1d8 {Modifier.Modify(constitutionScore.Value)})";
    }

    public override Tuple<string, int> GetPrimeRequisite()
    {
        string primerequisite = "Strength";
        var prScore = AbilityScores.FirstOrDefault(s => s.Key == primerequisite);
        int primeRequisiteScore = prScore.Value;
        return Tuple.Create(primerequisite, primeRequisiteScore);
    }
   
    
}
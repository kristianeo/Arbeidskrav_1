namespace Arbeidskrav_1.CharacterClasses;

public class Cleric(string charName, Dictionary<string, int> abilityScores) :
    CharacterClass("Cleric", charName, 1500, 1, 6, abilityScores)
{
    
    public override Tuple<string, int> GetPrimeRequisite()
    {
        string primerequisite = "Wisdom";
        var prScore = AbilityScores.FirstOrDefault(s => s.Key == primerequisite);
        int primeRequisiteScore = prScore.Value;
        return Tuple.Create(primerequisite, primeRequisiteScore);
    }
}
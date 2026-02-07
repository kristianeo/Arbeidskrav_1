namespace Arbeidskrav_1.CharacterClasses;

public class Fighter(string charName, Dictionary<string, int> abilityScores) :
    CharacterClass("Fighter", charName, 2000, 1, 8, abilityScores)
{
    public override Tuple<string, int> GetPrimeRequisite()
    {
        string primerequisite = "Strength";
        var prScore = AbilityScores.FirstOrDefault(s => s.Key == primerequisite);
        int primeRequisiteScore = prScore.Value;
        return Tuple.Create(primerequisite, primeRequisiteScore);
    }
   
    
}
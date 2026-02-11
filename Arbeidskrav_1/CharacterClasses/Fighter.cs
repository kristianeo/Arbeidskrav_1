namespace Arbeidskrav_1.CharacterClasses;

public class Fighter(string charName, Dictionary<string, int> abilityScores)
    : CharacterClass("Fighter", charName, 2000, abilityScores, _hitPoints)
{
    private static string _hitPoints = GetHitPoints("1d8");

    public override Tuple<string, int> GetPrimeRequisite()
    {
        string primerequisite = "Strength";
        var prScore = AbilityScores.FirstOrDefault(s => s.Key == primerequisite);
        int primeRequisiteScore = prScore.Value;
        return Tuple.Create(primerequisite, primeRequisiteScore);
    }
   
    
}
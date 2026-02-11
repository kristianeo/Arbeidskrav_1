namespace Arbeidskrav_1.CharacterClasses;

public class Fighter(string charName, Dictionary<string, int> abilityScores)
    : CharacterClass("Fighter", charName, 2000, abilityScores, _hitPoints)
{
    private static string _hitPoints = GetHitPoints("1d8");

    public override Tuple<string, int> GetPrimeRequisite()
    {
        const string primeRequisite = "Strength";
        var prScore = AbilityScores.FirstOrDefault(s => s.Key == primeRequisite);
        int primeRequisiteScore = prScore.Value;
        return Tuple.Create(primeRequisite, primeRequisiteScore);
    }
   
    
}
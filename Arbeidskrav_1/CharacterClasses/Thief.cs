namespace Arbeidskrav_1.CharacterClasses;

public class Thief(string charName, Dictionary<string, int> abilityScores)
    : CharacterClass("Thief", charName, 1200, abilityScores, _hitPoints)
{
    private static string _hitPoints = GetHitPoints("1d4");


    public override Tuple<string, int> GetPrimeRequisite()
    {
        string primerequisite = "Dexterity";
        var prScore = AbilityScores.FirstOrDefault(s => s.Key == primerequisite);
        int primeRequisiteScore = prScore.Value;
        return Tuple.Create(primerequisite, primeRequisiteScore);
    }
    
}
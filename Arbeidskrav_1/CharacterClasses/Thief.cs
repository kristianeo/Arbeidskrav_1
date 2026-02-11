namespace Arbeidskrav_1.CharacterClasses;

public class Thief(string charName, Dictionary<string, int> abilityScores)
    : CharacterClass("Thief", charName, 1200, abilityScores, _hitPoints)
{
    private static string _hitDice = ClassInfo[PrimeRequisite].Item2;
    
    private static string _hitPoints = GetHitPoints(_hitDice);
    
    private const string PrimeRequisite = "Dexterity";


    public override Tuple<string, int> GetPrimeRequisite()
    {
        var prScore = AbilityScores.FirstOrDefault(s => s.Key == PrimeRequisite);
        int primeRequisiteScore = prScore.Value;
        return Tuple.Create(PrimeRequisite, primeRequisiteScore);
    }
    
}
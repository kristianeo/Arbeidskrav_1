
namespace Arbeidskrav_1.CharacterClasses;

public class Cleric(string charName, Dictionary<string, int> abilityScores)
    : CharacterClass("Cleric", charName, 1500, abilityScores, _hitPoints)
{
    private static string _hitDice = ClassInfo[PrimeRequisite].Item2;
    
    private static string _hitPoints = GetHitPoints(_hitDice);
    
    private const string PrimeRequisite = "Wisdom";

    public override Tuple<string, int> GetPrimeRequisite()
    {
        var prScore = AbilityScores.FirstOrDefault(s => s.Key == PrimeRequisite);
        int primeRequisiteScore = prScore.Value;
        return Tuple.Create(PrimeRequisite, primeRequisiteScore);
    }
}
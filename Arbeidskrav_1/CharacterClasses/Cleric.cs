
namespace Arbeidskrav_1.CharacterClasses;

public class Cleric(string charName, Dictionary<string, int> abilityScores)
    : CharacterClass(ClassType, charName, 1500, abilityScores, _hitPoints)
{
    private const string ClassType = "Cleric";
    
    private const string PrimeRequisite = "Wisdom";
    
    private static string _hitDice = ClassInfo[ClassType].Item2;
    
    private static string _hitPoints = GetHitPoints(_hitDice);

    public override Tuple<string, int> GetPrimeRequisite()
    {
        var prScore = AbilityScores.FirstOrDefault(s => s.Key == PrimeRequisite);
        int primeRequisiteScore = prScore.Value;
        return Tuple.Create(PrimeRequisite, primeRequisiteScore);
    }
}
namespace Arbeidskrav_1.CharacterClasses;

public class Thief(string charName, Dictionary<string, int> abilityScores)
    : CharacterClass(ClassType, charName, 1200, abilityScores, _hitPoints)
{
    private const string ClassType = "Thief";
    
    private readonly string _primeRequisite = ClassInfo[ClassType].Item1;
    
    private static string _hitDice = ClassInfo[ClassType].Item2;
    
    private static string _hitPoints = GetHitPoints(_hitDice);

    public override Tuple<string, int> GetPrimeRequisite()
    {
        var prScore = AbilityScores.FirstOrDefault(s => s.Key == _primeRequisite);
        int primeRequisiteScore = prScore.Value;
        return Tuple.Create(_primeRequisite, primeRequisiteScore);
    }
    
}
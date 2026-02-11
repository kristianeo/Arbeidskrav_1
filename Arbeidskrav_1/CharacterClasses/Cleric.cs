using System.Runtime.CompilerServices;

namespace Arbeidskrav_1.CharacterClasses;

public class Cleric(string charName, Dictionary<string, int> abilityScores)
    : CharacterClass("Cleric", charName, 1500, abilityScores, _hitPoints)
{
    private static string _hitPoints = GetHitPoints("1d6");

    public override Tuple<string, int> GetPrimeRequisite()
    {
        const string primeRequisite = "Wisdom";
        var prScore = AbilityScores.FirstOrDefault(s => s.Key == primeRequisite);
        int primeRequisiteScore = prScore.Value;
        return Tuple.Create(primeRequisite, primeRequisiteScore);
    }
}
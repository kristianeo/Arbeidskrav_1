namespace Arbeidskrav_1.CharacterClasses;

public class MagicUser(string charName, Dictionary<string, int> abilityScores)
    : CharacterClass("Magic User", charName, 2500, abilityScores, _hitPoints)
{
    private static string _hitPoints = GetHitPoints("1d4");

    public override Tuple<string, int> GetPrimeRequisite()
    {
        string primerequisite = "Intelligence";
        var prScore = AbilityScores.FirstOrDefault(s => s.Key == primerequisite);
        int primeRequisiteScore = prScore.Value;
        return Tuple.Create(primerequisite, primeRequisiteScore);
    }
}
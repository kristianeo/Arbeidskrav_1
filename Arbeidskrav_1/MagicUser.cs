namespace Arbeidskrav_1;

public class MagicUser(string charName) :CharacterClass("Magic User", "Intelligence", charName, 2500, 1, 4)
{
    public static Dictionary<string, int> Abilities = new()
    {
        { "Strength", 0 },
        { "Intelligence", 0 },
        { "Wisdom", 0 },
        { "Dexterity", 0 },
        { "Constitution", 0 },
        { "Charisma", 0 }
    };
    
    public override string ToString()
    {
        return this.ClassName;
    }
}
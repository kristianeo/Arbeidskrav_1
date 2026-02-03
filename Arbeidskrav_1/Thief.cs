namespace Arbeidskrav_1;

public class Thief(string charName) :CharacterClass("Thief", "Dexterity", charName, 1200, 1, 4)
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
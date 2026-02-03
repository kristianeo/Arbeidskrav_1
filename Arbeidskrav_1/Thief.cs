namespace Arbeidskrav_1;

public class Thief:CharacterClass
{
    public Thief():base("Thief", "Dexterity")
    {
        
    }
    
    public Dictionary<string, int> ThiefAbilities = new()
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
        return this.Name;
    }
}
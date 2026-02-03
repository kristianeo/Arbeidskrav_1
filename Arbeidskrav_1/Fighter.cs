namespace Arbeidskrav_1;

public class Fighter:CharacterClass
{
    public Fighter():base("Fighter", "Strength")
    {
        
    }
    
    public Dictionary<string, int> FighterAbilities = new()
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
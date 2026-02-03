namespace Arbeidskrav_1;

public class MagicUser:CharacterClass
{
    public MagicUser():base("Magic User", "Intelligence")
    {
        
    }
    
    public Dictionary<string, int> MagicAbilities = new()
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
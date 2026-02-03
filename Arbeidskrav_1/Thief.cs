namespace Arbeidskrav_1;

public class Thief:CharacterClass
{
    public Thief():base("Thief", "Dexterity")
    {
        
    }
    
    public override string ToString()
    {
        return this.Name;
    }
}
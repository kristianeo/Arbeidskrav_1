namespace Arbeidskrav_1;

public class MagicUser:CharacterClass
{
    public MagicUser():base("Magic User", "Intelligence")
    {
        
    }
    
    public override string ToString()
    {
        return this.Name;
    }
}
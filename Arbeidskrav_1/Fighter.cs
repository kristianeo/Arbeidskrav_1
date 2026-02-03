namespace Arbeidskrav_1;

public class Fighter:CharacterClass
{
    public Fighter():base("Fighter", "Strength")
    {
        
    }
    
    public override string ToString()
    {
        return this.Name;
    }
}
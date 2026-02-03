namespace Arbeidskrav_1;

public class Cleric:CharacterClass
{
    public Cleric() : base("Cleric", "Wisdom")
    {
        
    }

    private int _diceroll = Diceroll(1, 6);

    public override string ToString()
    {
        return this.Name;
    }
}
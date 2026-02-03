namespace Arbeidskrav_1;

public class Cleric:CharacterClass
{
    public Cleric() : base("Cleric", "Wisdom")
    {
        
    }

    private int _diceroll = Diceroll(1, 6);
    
    public static Dictionary<string, int> ClericAbilities = new()
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
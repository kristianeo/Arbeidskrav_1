namespace Arbeidskrav_1.CharacterClasses;

public class DiceRoll
{
    public static int RollDice(int dice, int sides)
    {
        int diceRoll = 0;
        Random random = new Random();
        for (int d = 0; d < dice; d ++)
        {
            diceRoll += random.Next(1, sides+1);
        }
        return diceRoll;
    }
}
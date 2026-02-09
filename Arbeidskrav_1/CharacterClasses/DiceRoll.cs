namespace Arbeidskrav_1.CharacterClasses;

public class DiceRoll
{
    public static int RollDice(string input)
    {
        if (!input.ToLower().Contains('d'))
        {
            throw new InvalidDataException("No 'd' in the dice input");
        }

        string[] parts = input.ToLower().Split('d');

        int count = int.Parse(parts[0]);
        int dieType = int.Parse(parts[1]);
        int diceRoll = 0;
        Random random = new Random();

        for (int d = 0; d < count; d ++)
        {
            diceRoll += random.Next(1, dieType+1);
        }
        return diceRoll;
    }
}
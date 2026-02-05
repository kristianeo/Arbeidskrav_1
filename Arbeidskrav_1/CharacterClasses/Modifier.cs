namespace Arbeidskrav_1.CharacterClasses;

public class Modifier
{
    public static string Modify(int num)
    {
        string modifier = num switch
        {
            3 => "-3",
            4 or 5 => "-2",
            6 or 7 or 8 => "-1",
            9 or 10 or 11 or 12 => "+0",
            13 or 14 or 15 => "+1",
            16 or 17 => "+2",
            18 => "+3",
            _ => "0"
        };

        return modifier;
    }
}
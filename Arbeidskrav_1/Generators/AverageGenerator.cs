namespace Arbeidskrav_1.Generators;

public abstract class AverageGenerator
{
    public static int Average(Dictionary<string, int> dictionary)
    {
        int total = dictionary.Keys.Sum(value => dictionary[value]);
        int average = total / dictionary.Count;
        return average;
    }
}
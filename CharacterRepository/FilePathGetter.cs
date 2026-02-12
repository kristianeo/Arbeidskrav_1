namespace Arbeidskrav_1.CharacterRepository;

public abstract class FilePathGetter
{
    public static string GetFilePath()
    {
        const string filePath = @"..\Arbeidskrav_1\CharacterRepository\CharacterRepo.json";
        return filePath;
    }
}
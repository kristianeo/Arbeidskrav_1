namespace Arbeidskrav_1;

class Program
{
    static void Main(string[] args)
    {
        bool choice = RunGenerator.WelcomeMessage();
        if (choice)
        {
            RunGenerator.Run();
        }
        else
        {
            CharacterRepository.CharacterGetter.GetCharacter();
        }
    }
}
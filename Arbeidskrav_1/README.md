put comments on public properties and methods. 
entering character name or randomly generate one.

dotnet add package Newtonsoft.Json

Sources: 
- [Build a dictionary](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/collections)
- [Find the second largest element](https://www.geeksforgeeks.org/dsa/find-second-largest-element-array/)
- [Find the value when knowing the key](https://stackoverflow.com/questions/22505998/how-to-get-value-from-dictionary-without-knowing-key)
- [Format dictionary](https://stackoverflow.com/questions/3611126/using-todictionary)
- [Exit terminal](https://stackoverflow.com/questions/10286056/what-is-the-command-to-exit-a-console-application-in-c)

Use of AI:
I used AI with help to create the .json file to store saved characters, and to create a method to display a saved character from the repo.
I changed the method to use a dictionary instead of CharacterClass character, as not all values are stored as properties.
I did not use AI for the rest of this project. 

Prompt: why isnt this adding the text to the CharacterRepo.json file?     public static void AddToRepo(CharacterClass character) { string _jsonString = JsonSerializer.Serialize(character); File.WriteAllText("CharacterRepo.json", _jsonString);
}
Answer used: Ah, Kristiane — this is a classic “why isn’t my JSON file growing?” moment. And the answer is simple but sneaky:
You're overwriting the file every single time.
Your current code:
File.WriteAllText("CharacterRepo.json", _jsonString);


WriteAllText replaces the entire file with whatever you pass in. So instead of adding a new character, you're nuking the old content and writing only the new one.

✅ What you actually want
A JSON file that contains a list of characters, and each time you add one, it appends to that list.
To do that, you need to:
- Read the existing JSON file (if it exists)
- Deserialize it into a List<CharacterClass>
- Add the new character to the list
- Serialize the list back to JSON
- Write it back to the file

⭐ Corrected version
public static void AddToRepo(CharacterClass character)
{
const string filePath = "CharacterRepo.json";

    List<CharacterClass> characters = new();

    if (File.Exists(filePath))
    {
        string existingJson = File.ReadAllText(filePath);

        if (!string.IsNullOrWhiteSpace(existingJson))
        {
            characters = JsonSerializer.Deserialize<List<CharacterClass>>(existingJson)
                         ?? new List<CharacterClass>();
        }
    }

    characters.Add(character);

    string updatedJson = JsonSerializer.Serialize(
        characters,
        new JsonSerializerOptions { WriteIndented = true }
    );

    File.WriteAllText(filePath, updatedJson);
}



💡 Why this works
- It preserves existing characters.
- It ensures the file always contains a JSON array, not a single object.
- It handles empty or missing files gracefully.
- It formats the JSON nicely for readability.

If you want, I can help you build a full CharacterRepository class with load/save methods, validation, and async support — or even wire it into your Spectre.Console UI so adding characters feels smooth and polished.


Prompt: there is still no text in the json file after using the code

Answer used: Alright, Kristiane — if the JSON file is still completely empty, then we’re no longer dealing with a logic issue. Your serialization code is correct. That means something deeper is going wrong.
Let’s walk through the real reasons this happens in C#, and I’ll help you pinpoint the exact culprit.

//I jumped straight to this as I knew the code ran:
🎯 Let’s diagnose it fast
Tell me:
What does this print?
Console.WriteLine(Directory.GetCurrentDirectory());


Prompt: C:/Users/olsen/OneDrive/Dokumenter/GitHub/Arbeidskrav_1/Arbeidskrav_1/bin/Debug/net8.0/Arbeidskrav_1.exe this is the current directory

Answer: 
Great — that tells us exactly what’s going on.
Your program is writing the JSON file to:
C:/Users/olsen/OneDrive/Dokumenter/GitHub/Arbeidskrav_1/Arbeidskrav_1/bin/Debug/net8.0/


…but you are probably opening the JSON file in your project root, not the one inside bin/Debug/net8.0.
This is the most common cause of “my JSON file is empty” — the program is writing to one file, and you’re checking a different one.

✅ How to confirm instantly
Go to this folder:
C:/Users/olsen/OneDrive/Dokumenter/GitHub/Arbeidskrav_1/Arbeidskrav_1/bin/Debug/net8.0/


You will find CharacterRepo.json there, and it will contain the data.

I used this:
⭐ Option B — Use a fixed absolute path (simple)
const string filePath = @"C:\Users\olsen\OneDrive\Dokumenter\GitHub\Arbeidskrav_1\Arbeidskrav_1\CharacterRepo.json";




Prompt: is it possible to save different data to the json file without ut being Properties of the class?

Answer used: ✅ Option 2 — Store arbitrary key/value data
If you want total flexibility, use a dictionary:
Dictionary<string, object> data = new();


Example JSON:
{
"PlayerName": "Kristiane",
"HighScore": 4200,
"UnlockedClasses": ["Mage", "Ranger"],
"LastCharacter": {
"Name": "Luna",
"Class": "Cleric"
}
}


C# code:
var json = JsonSerializer.Serialize(data, new JsonSerializerOptions
{
WriteIndented = true
});

Prompt: is it possible to extract the data from the json file, but one specific character?

Answer: ✅ Extract a specific character by name (or any property)
public static CharacterClass? GetCharacter(string name)
{
const string filePath = "CharacterRepo.json";

    if (!File.Exists(filePath))
        return null;

    string json = File.ReadAllText(filePath);

    if (string.IsNullOrWhiteSpace(json))
        return null;

    List<CharacterClass>? characters =
        JsonSerializer.Deserialize<List<CharacterClass>>(json);

    if (characters == null)
        return null;

    return characters.FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
}



Prompt: Is it possible to make it invalid to enter a character name already used?

Answer: Absolutely — and this is one of those little UX touches that makes your program feel polished.
Yes, you can make it invalid to enter a character name that already exists, and you can enforce it cleanly.
There are two good ways to do it:

✅ Option 1 — Check the JSON file before adding
This is the simplest and most reliable approach.
Add a method like this:
public static bool CharacterExists(string name)
{
const string filePath = "CharacterRepo.json";

    if (!File.Exists(filePath))
        return false;

    string json = File.ReadAllText(filePath);

    if (string.IsNullOrWhiteSpace(json))
        return false;

    var characters = JsonSerializer.Deserialize<List<CharacterClass>>(json);

    if (characters == null)
        return false;

    return characters.Any(c => 
        c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
}


Then modify your add method:
public static void AddToRepo(CharacterClass character)
{
if (CharacterExists(character.Name))
{
Console.WriteLine("A character with that name already exists.");
return;
}

    // existing add logic...
}



⭐ Option 2 — Validate before creating the CharacterClass
If you’re using Spectre.Console or a menu system, you can block the user before they even finish creating the character.
Example:
string name;

do
{
name = AnsiConsole.Ask<string>("Enter character name:");

    if (CharacterExists(name))
    {
        AnsiConsole.MarkupLine("[red]That name is already taken. Try another.[/]");
    }

} while (CharacterExists(name));


This gives a smooth user experience.


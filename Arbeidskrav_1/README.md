# OSE Character Generator

A terminal-based program where you can create your own character based on a simplified version of [Old-School Essentials - Basic Rules v1.4](https://necroticgnome.com/products/old-school-essentials-basic-rules).

## Table of Contents

- [Features](#features)
- [Technologies Used](#technologies-used)
- [Author](#author)
- [Process](#process)
- [Sources](#sources)
- [Use of AI](#use-of-ai)

## Features

- **Interactive Terminal UI**: Interesting terminal with key navigation
- **Two modes**: Create a character or search for a character in the repository
- **Try again**: Choose not to save your generated character 

## Technologies Used

- **C# 12**
- **Target framework**: .NET8, Spectre.Console would not work in Rider with newer version on my Windows computer
- **Spectre.Console**: System for making terminal applications
- **System.Text.Json**: Used to convert C# data to .json and back

## Author
Kristiane Olsen  [GitHub](https://github.com/Kristianeo)

## Process
I started with making an abstract CharacterClass as the base class, and then the concrete classes. 
It took a while to figure out which values should be properties and which should be overridden methods. 
In the start I also added the DiceRoll and Modifier classes, and have done minimal modifications to these since.

The next step was adding the AbilityScoreGenerator, where I also made the AverageGenerator. Originally the UnderAverageHandler
was in the GenerateAbilityScores method, but I made this a class of its own to keep the methods simple. 
I then added AvailableClassesGenerator to have a method to store the available classes based on the criteria, which
includes NoAvailableClassesChecker in case the two highest prime requisite scores are those of Charisma and Constitution.
The ClassSelector allows for the player to choose between the available classes (or get a notification there is only one available class).
When the player has chosen a class, the CharacterGenerator creates an instance of the chosen class, also using CharacterNameGenerator
to enter a name. 

After this was implemented, I modified the code to support Spectre.Console for a more interesting terminal experience.
I added classes for displaying the Ability scores and the character created. 

I added the CharacterRepository, with the .json to store the saved characters and classes to save/retrieve the characters.
This is the only part of the project in which I used AI for help creating the code.
I chose to use a dictionary as the template for saving the characters, as all the information I wanted to store were not properties of the concrete classes.
That is why the CharacterRepository has its own CharacterDisplayer class. Finally, I added a welcome screen where the player can choose 
to create a new character or search the repo, and CreateOrSearch which asks the player what they would like to do next. 

## Sources
- [Build a dictionary](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/collections)
- [Find the second-largest element](https://www.geeksforgeeks.org/dsa/find-second-largest-element-array/)
- [Find the value when knowing the key](https://stackoverflow.com/questions/22505998/how-to-get-value-from-dictionary-without-knowing-key)
- [Format dictionary](https://stackoverflow.com/questions/3611126/using-todictionary)
- [Exit terminal](https://stackoverflow.com/questions/10286056/what-is-the-command-to-exit-a-console-application-in-c)
- [Using System.Text.Json](https://blog.nashtechglobal.com/read-and-write-json-file-in-net/)

## Use of AI
I used AI to help with how to store character data in .json, and to create a method to display a saved character from the repo.
I changed the method to use a dictionary instead of CharacterClass character, as not all values are stored as properties.
I did not use AI for the rest of this project. 

### Prompt: 
why isnt this adding the text to the CharacterRepo.json file?     

    public static void AddToRepo(CharacterClass character) { string _jsonString = JsonSerializer.Serialize(character); File.WriteAllText("CharacterRepo.json", _jsonString);
    }

### Answer used: 
Ah, Kristiane — this is a classic “why isn’t my JSON file growing?” moment. And the answer is simple but sneaky:
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

💡 Why this works
- It preserves existing characters.
- It ensures the file always contains a JSON array, not a single object.
- It handles empty or missing files gracefully.
- It formats the JSON nicely for readability.

### Prompt: 
is it possible to save different data to the json file without ut being Properties of the class?

### Answer used: 
✅ Option 2 — Store arbitrary key/value data
If you want total flexibility, use a dictionary:
Dictionary<string, object> data = new();


Example JSON:

    {
    "PlayerName": "Kristiane",
    "HighScore": 4200,
    "UnlockedClasses": ["Mage", "Ranger"],
    "LastCharacter": 
    {
        "Name": "Luna",
        "Class": "Cleric"
    }
    }
        
    C# code:
    var json = JsonSerializer.Serialize(data, new JsonSerializerOptions
    {
    WriteIndented = true
    });

### Prompt: 
is it possible to extract the data from the json file, but one specific character?

### Answer:
✅ Extract a specific character by name (or any property)

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

### Prompt: 
Is it possible to make it invalid to enter a character name already used?

### Answer: 
Absolutely — and this is one of those little UX touches that makes your program feel polished.
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


This gives a smooth user experience.


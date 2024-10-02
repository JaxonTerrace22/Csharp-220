#nullable disable
using System;
using System.Collections.Generic;
using System.IO;

public class Entry
{
    public string Prompt { get; private set; }
    public string Response { get; private set; }
    public string Date { get; private set; }

    public Entry(string prompt, string response, string date = null)
    {
        Prompt = prompt;
        Response = response;
        Date = date ?? DateTime.Now.ToShortDateString();
    }

    public override string ToString()
    {
        return $"Date: {Date}\nPrompt: {Prompt}\nResponse: {Response}\n";
    }
}

public class Journal
{
    private List<Entry> entries = new List<Entry>();
    private List<string> prompts = new List<string>
    {
        "Did I go to work today?",
        
        "Did I brush my teeth today?",
        
        "How much work did I accomplish on my yard today?",
        
        "Did I feed my pets today?",
        
        "Did I clean my room today?"
    };

    public void AddEntry()
    {
        Random random = new Random();
        
        int index = random.Next(prompts.Count);
        
        string prompt = prompts[index];

        Console.WriteLine(prompt);
        string response = Console.ReadLine();

        entries.Add(new Entry(prompt, response));  
    }

    public void DisplayEntries()
    {
        foreach (var entry in entries)
        {
            Console.WriteLine(entry.ToString());
        }
    }

    public void SaveToFile(string filename)
    {
        try
        {
            using (var writer = new StreamWriter(filename))
            {
                foreach (var entry in entries)
                {
                    writer.WriteLine($"{entry.Date}~|~{entry.Prompt}~|~{entry.Response}");
                }
            }
            Console.WriteLine("Journal entry saved.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Journal did not save: {ex.Message}");
        }
    }

    public void LoadFromFile(string filename)
    {
        try
        {
            entries.Clear();
            var lines = File.ReadAllLines(filename);

            foreach (string line in lines)
            {
               string[] parts = line.Split(new string[] { "~|~" }, StringSplitOptions.None);

                if (parts.Length == 3)
                {
                    entries.Add(new Entry(parts[1], parts[2], parts[0]));
                }
            }
            Console.WriteLine("Journal loaded successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to load journal: {ex.Message}");
        }
    }
}

public class Program
{
    static void Main(string[] args)
    {
        var journal = new Journal();
        bool running = true;

        while (running)
        {
            Console.WriteLine("Please select one of the following choices:");
            
            Console.WriteLine("1. New Entry");
            
            Console.WriteLine("2. Show Previous Entries");
            
            Console.WriteLine("3. Save Journal");
            
            Console.WriteLine("4. Load Journal");
            
            Console.WriteLine("5. Quit");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    journal.AddEntry();
                    break;
                
                case "2":
                    journal.DisplayEntries();
                    break;
                
                case "3":
                    Console.WriteLine("Enter the Journal Entry name to save:");
                    string saveFilename = Console.ReadLine();
                    journal.SaveToFile(saveFilename);
                    break;
                
                case "4":
                    Console.WriteLine("Enter the Journal name to load:");
                    string loadFilename = Console.ReadLine();
                    journal.LoadFromFile(loadFilename);
                    break;
                
                case "5":
                    running = false;
                    break;
                
                default:
                    Console.WriteLine("Please pick a different option.");
                    break;
            }
        }
    }
}

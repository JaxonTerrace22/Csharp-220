#nullable disable
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

[Serializable]
[JsonDerivedType(typeof(SimpleGoal), "Short-termGoal")]

[JsonDerivedType(typeof(EternalGoal), "EternalGoal")]

[JsonDerivedType(typeof(ChecklistGoal), "ChecklistGoal")]
public abstract class Goal
{
    public string Description { get; set; }
    public int Points { get; set; }
    public bool IsComplete { get; set; }

    protected Goal(string description, int points)
    {
        Description = description;
        Points = points;
        IsComplete = false;
    }

    public abstract void RecordProgress();
    public abstract void Display();
}

[Serializable]
public class SimpleGoal : Goal
{
    public SimpleGoal(string description, int points) : base(description, points) { }

    public override void RecordProgress()
    {
        IsComplete = true;
        Console.WriteLine($"Completed: {Description}, Points Earned: {Points}");
    }

    public override void Display()
    {
        Console.WriteLine($"{Description} - [{(IsComplete ? "X" : " ")}]");
    }
}

[Serializable]
public class EternalGoal : Goal
{
    public int TimesCompleted { get; set; }

    public EternalGoal(string description, int pointsPerCompletion)
        : base(description, pointsPerCompletion) { }

    public override void RecordProgress()
    {
    TimesCompleted++;
    Console.WriteLine($"Finished {TimesCompleted} times: {Description}, Points Earned: {Points}");
    }

    public override void Display()
    {
        Console.WriteLine($"{Description} - Finished {TimesCompleted} times");
    }
}

[Serializable]
public class ChecklistGoal : Goal
{
    public int TargetCompletions { get; set; }
    public int CurrentCompletions { get; set; }
    public int BonusPoints { get; set; }

    public ChecklistGoal(string description, int pointsPerCompletion, int targetCompletions, int bonusPoints)
        : base(description, pointsPerCompletion)
    {
        TargetCompletions = targetCompletions;
        
        CurrentCompletions = 0;
        
        BonusPoints = bonusPoints;
    }

    public override void RecordProgress()
    {
        CurrentCompletions++;
        if (CurrentCompletions >= TargetCompletions)
        {
            IsComplete = true;
            Console.WriteLine($"Goal completed: {Description}, Total Points Earned: {Points * CurrentCompletions + BonusPoints}");
        }
        else
        {
            Console.WriteLine($"Progress: {CurrentCompletions}/{TargetCompletions} for {Description}, Points Earned: {Points}");
        }
    }

    public override void Display()
    {
        Console.WriteLine($"{Description} - [{CurrentCompletions}/{TargetCompletions}] {(IsComplete ? "[X]" : "")}");
    }
}

class Program
{
    static List<Goal> goals = new List<Goal>();
    static int totalPoints = 0;

    static void Main()
    {
        LoadGoals();
        Console.WriteLine("Welcome to Eternal Quest Goal Tracker!");

        string userInput;
        do
        {
            Console.WriteLine("\nSelect an option:");
            
            Console.WriteLine("1. Add a new Goal");
            
            Console.WriteLine("2. Log a Goal Progress");
            
            Console.WriteLine("3. Show Goals");
            
            Console.WriteLine("4. Save and Quit");

            userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    
                    AddGoal();
                    break;
                
                case "2":
                    
                    RecordProgress();
                    break;
                
                case "3":
                    
                    DisplayGoals();
                    break;
                
                case "4":
                    
                    SaveGoals();
                    break;
                
                default:
                    
                    Console.WriteLine("Incorrect option, try again.");
                    break;
            }
        } while (userInput != "4");
    }

    static void AddGoal()
    {
        Console.WriteLine("Choose a goal type: 1. Short-term 2. Eternal 3. Checklist");
        string type = Console.ReadLine();

        Console.Write("Enter description: ");
        string description = Console.ReadLine();

        Console.Write("Enter points: ");
        int points = int.Parse(Console.ReadLine());

        switch (type)
        {
            case "1":
                
                goals.Add(new SimpleGoal(description, points));
                break;
            
            case "2":
                goals.Add(new EternalGoal(description, points));
                break;
            case "3":
                
                Console.Write("Enter target completions: ");
                int target = int.Parse(Console.ReadLine());

                Console.Write("Enter bonus points: ");
                
                int bonus = int.Parse(Console.ReadLine());

                goals.Add(new ChecklistGoal(description, points, target, bonus));
                break;
            default:
                Console.WriteLine("Incorrect goal type.");
                break;
        }
    }

    static void RecordProgress()
    {
        Console.WriteLine("Choose a goal to log progress:");
        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {goals[i].Description}");
        }

        if (int.TryParse(Console.ReadLine(), out int goalIndex) &&
            goalIndex > 0 && goalIndex <= goals.Count)
        {
            Goal goal = goals[goalIndex - 1];
            
            goal.RecordProgress();
            totalPoints += goal.Points;
            
        Console.WriteLine($"Total Points: {totalPoints}");
        }
        else
        {
            Console.WriteLine("Incorrect input.");
        }
    }

    static void DisplayGoals()
    {
        foreach (var goal in goals)
        {
            goal.Display();
        }
    }

    static void LoadGoals()
    {
        if (File.Exists("goals.json"))
        {
            string json = File.ReadAllText("goals.json");
            
            goals = JsonSerializer.Deserialize<List<Goal>>(json, new JsonSerializerOptions
            {
                IncludeFields = true
            }) ?? new List<Goal>();
        }
    }

    static void SaveGoals()
    {
        string json = JsonSerializer.Serialize(goals, new JsonSerializerOptions
        {
            IncludeFields = true,
            WriteIndented = true
        });
        File.WriteAllText("goals.json", json);
        Console.WriteLine("Goals recorded successfully.");
    }
}

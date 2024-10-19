#nullable disable
using System;
using System.Collections.Generic;
using System.Threading;

public class BaseActivity
{
    protected string Name { get; set; }
    protected string Description { get; set; }
    private HashSet<string> usedPrompts = new HashSet<string>();

    public BaseActivity(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public void Run(int duration)
    {
        StartActivity(duration);
        PerformActivity(duration);
        EndActivity();
    }

    protected virtual void PerformActivity(int duration) { }

    private void StartActivity(int duration)
    {
        Console.WriteLine($"Starting {Name}");
        Console.WriteLine(Description);
        Console.WriteLine($"Your activity will last for {duration} seconds.");
        DisplayCountdown(3); 
    }

    private void EndActivity()
    {
        Console.WriteLine("Congratulations! You have finished your activity.");
    }

    protected string GetUniquePrompt(List<string> prompts)
    {
        List<string> availablePrompts = new List<string>(prompts);
        availablePrompts.RemoveAll(p => usedPrompts.Contains(p));
        if (availablePrompts.Count == 0)
        {
            usedPrompts.Clear();
            availablePrompts = new List<string>(prompts);
        }
        string prompt = availablePrompts[new Random().Next(availablePrompts.Count)];
        usedPrompts.Add(prompt);
        return prompt;
    }

    protected void DisplayCountdown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write("\rPreparing... " + new string('.', 3 - i % 3) + "   ");  
            Thread.Sleep(1000);
        }
        Console.WriteLine("\r"); 
    }
}

public class BreathingActivity : BaseActivity
{
    public BreathingActivity() : base("For Your Breathing Activity", "Focus on your breathing, feel the breaths entering and leaving.")
    {
    }

    protected override void PerformActivity(int duration)
    {
        var startTime = DateTime.Now;
        while ((DateTime.Now - startTime).TotalSeconds < duration)
        {
            Console.WriteLine("Breathe in...");
            Thread.Sleep(2000);
            
            Console.WriteLine("Breathe out...");
            Thread.Sleep(2000);
        }
    }
}

public class ReflectionActivity : BaseActivity
{
    private List<string> questions = new List<string>
    {
        "Why was this experience important or special for you?",
        "How did you feel after it was over?"
    };

    public ReflectionActivity() : base("For Your Reflection Activity", "Reflect on a personal experience where you felt yourself being powerful or strong.")
    {
    }

    protected override void PerformActivity(int duration)
    {
        var startTime = DateTime.Now;
        while ((DateTime.Now - startTime).TotalSeconds < duration)
        {
            string question = GetUniquePrompt(questions);
            Console.WriteLine(question);
            DisplayCountdown(5);  
        }
    }
}

public class ListingActivity : BaseActivity
{
    public ListingActivity() : base("For Your Listing Activity", "Write down all of your strong and positive attributes that you possess.")
    {
    }

    protected override void PerformActivity(int duration)
    {
        var startTime = DateTime.Now;
        int count = 0;
        while ((DateTime.Now - startTime).TotalSeconds < duration)
        {
            Console.WriteLine("Write something positive about yourself:");
            Console.ReadLine();
            count++;
        }
        Console.WriteLine($"You wrote down {count} attributes.");
    }
}

public class MusicRelaxationActivity : BaseActivity
{
    public MusicRelaxationActivity() : base("For Your Music Activity", "Listen to some calming music that relaxes your mind.")
    {
    }

    protected override void PerformActivity(int duration)
    {
        Console.WriteLine("Playing calming music...");
        Thread.Sleep(duration);  
    }
}

class Program
{
    static void Main()
    {
        var activities = new Dictionary<string, BaseActivity>
        {
            { "breathing", new BreathingActivity() },
            
            { "reflection", new ReflectionActivity() },
            
            { "listing", new ListingActivity() },
            
            { "music", new MusicRelaxationActivity() }
        };

        Console.WriteLine("Choose an activity to do (Breathing, Reflection, Listing, Music): ");
        string activityType = Console.ReadLine().ToLower();
        
        Console.WriteLine("Enter the length you want to pend on the activity in seconds: ");
        int duration = int.Parse(Console.ReadLine());

        if (activities.ContainsKey(activityType))
        {
            activities[activityType].Run(duration);
        }
        else
        {
            Console.WriteLine("Incorrect activity type.");
        }
    }
}

using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
    List<Activity> activities = new List<Activity>()
        {
            new Running(new DateTime(2022, 11, 3), 30, 3.0),
            
            
            new Cycling(new DateTime(2022, 11, 3), 30, 20.0),
            
            
            new Swimming(new DateTime(2022, 11, 3), 30, 12)
        };

        foreach (var activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}

public abstract class Activity
{
    private DateTime date;
    private double minutes; 

    public Activity(DateTime date, double minutes)
    {
        this.date = date;
        this.minutes = minutes;
    }

    protected double Minutes 
    {
        get { return minutes; }
    }

    public abstract double GetDistance();
    
    public abstract double GetSpeed();
    
    public abstract double GetPace();

    public virtual string GetSummary()
    {
        return $"{date:dd MMM yyyy} ({Minutes} min)- Distance: {GetDistance()} miles, Speed: {GetSpeed()} mph, Pace: {GetPace()} min per mile";
    }
}

public class Running : Activity
{
    private double distance;

    public Running(DateTime date, double minutes, double distance) : base(date, minutes)
    {
           this.distance = distance;
    }

    public override double GetDistance()
    {
        
        return distance;
    }

    public override double GetSpeed()
    {
        return (distance / Minutes) * 60; 
    }

    public override double GetPace()
    {
        return Minutes / distance; 
    }
}

public class Cycling : Activity

{
    private double speed;

    public Cycling(DateTime date, double minutes, double speed) : base(date, minutes)
    {
        this.speed = speed;
    }

    public override double GetDistance()
    {
        return (speed * Minutes) / 60; 
    }

    
    public override double GetSpeed()
    {
        return speed;
    }

    public override double GetPace()
    {
        return 60 / speed;
    }
}



public class Swimming : Activity
{
    private int laps;

        public Swimming(DateTime date, double minutes, int laps) : base(date, minutes)
    {
        this.laps = laps;
    }

    public override double GetDistance()
    {
        return laps * 50 / 1000.0 * 0.62; 
    }

   
    public override double GetSpeed()
    {
        return (GetDistance() / Minutes) * 60; 
    }

        public override double GetPace()
    {
        return Minutes / GetDistance(); 
    }
}

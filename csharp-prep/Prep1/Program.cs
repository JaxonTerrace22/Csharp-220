#nullable disable
public class Prep1
{
    static void Main(string[] args)
    {
        Console.Write("First Name? ");
        string first = Console.ReadLine();

        Console.Write("Last Name? ");
        string last = Console.ReadLine();

        Console.WriteLine($"Your name is {last}, {first} {last}.");
    }
}

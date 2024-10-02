#nullable disable

using System;

public class Reference
{
    private string book;
        private int chapter;
            private int verseStart;
        private int verseEnd;

    public Reference(string book, int chapter, int verse)
    {
        this.book = book;
        
            this.chapter = chapter;
        
        this.verseStart = verse;
        
            this.verseEnd = verse;
    }

    public Reference(string book, int chapter, int verseStart, int verseEnd)
    {
        this.book = book;
        
        this.chapter = chapter;
        
        this.verseStart = verseStart;
        
        this.verseEnd = verseEnd;
    }

    public string GetDisplayText()
    {
        if (verseStart == verseEnd)
        {
            return $"{book} {chapter}:{verseStart}";
        }
        else
        {
            return $"{book} {chapter}:{verseStart}-{verseEnd}";
        }
    }
}

public class Word
{
    private string text;
    private bool isHidden;

    public Word(string text)
    {
        this.text = text;
        isHidden = false;
    }

    public void Hide()
    {
        isHidden = true;
    }

    public void Show()
    {
        isHidden = false;
    }

    public string GetDisplayText()
    
    {
        if (isHidden)
        {
            return new string('_', text.Length);
        }
        
        else
        {
            return text;
        }
    }

    public bool IsHidden()
    {
        return isHidden;
    }
}

public class Scripture
{
    private Reference reference;
    private List<Word> words;

    public Scripture(Reference reference, string text)
    {
        this.reference = reference;
        
        words = new List<Word>();
        
        string[] wordArray = text.Split(' ');
        
        foreach (string word in wordArray)
        {
            words.Add(new Word(word));
        }
    }

    public void HideRandomWords()
    {
        Random rand = new Random();
        int wordsToBeHidden = rand.Next(1, 4);

        for (int i = 0; i < wordsToBeHidden; i++)
        {
            bool HiddenWords = false;

            while (!HiddenWords)
            {
                int randomIndex = rand.Next(words.Count);
                
                if (!words[randomIndex].IsHidden())
                {
                    words[randomIndex].Hide();
                    
                    HiddenWords = true;
                }
            }
        }
    }

    public void DisplayScripture()
    {
        Console.Clear();
        
          Console.WriteLine(reference.GetDisplayText());
          foreach (Word word in words)
        {
            Console.Write(word.GetDisplayText() + " ");
        }
            Console.WriteLine();
    }

    public bool AreAllWordsHidden()
    {
        foreach (Word word in words)
        {
        if (!word.IsHidden())
        {
                return false;
        }
        }
        return true;
    }

    public bool CheckGuess(string guess)
    {
        foreach (Word word in words)
        {
            if (word.GetDisplayText() == new string('_', guess.Length) && word.ToString().ToLower() == guess.ToLower())
            {
                word.Show();
                return true;
            }
        }
        return false;
    }

    public static List<Scripture> LoadFromFile(string filePath)
    {
        List<Scripture> scriptures = new List<Scripture>();
        string[] lines = File.ReadAllLines(filePath);

        for (int i = 0; i < lines.Length; i += 2)
        {
            string referenceLine = lines[i];
            string scriptureLine = lines[i + 1];

            string[] referenceParts = referenceLine.Split(' ');
            
            string book = referenceParts[0];
            
            
            string[] chapterAndVerses = referenceParts[1].Split(':');

            int chapter = int.Parse(chapterAndVerses[0]);
            string[] verses = chapterAndVerses[1].Split('-');
            
            int verseStart = int.Parse(verses[0]);
            int verseEnd = verses.Length > 1 ? int.Parse(verses[1]) : verseStart;

            Reference reference = new Reference(book, chapter, verseStart, verseEnd);
            
            
            scriptures.Add(new Scripture(reference, scriptureLine));
        }

        return scriptures;
    }
}

public class Program
{
    public static void Main(string[] args)
    {
       
        List<Scripture> scriptures = Scripture.LoadFromFile("scriptures.txt");

       
        Random rand = new Random();
        Scripture scripture = scriptures[rand.Next(scriptures.Count)];

        while (true)
        {
            scripture.DisplayScripture();

            Console.WriteLine("\nPress Enter to hide words, type 'guess' to guess hidden words, or type 'exit' to quit:");
            string userInput = Console.ReadLine().ToLower();

            if (userInput == "exit")
            {
                break;
            }
            else if (userInput == "guess")
            {
                Console.WriteLine("guess your hidden word:");
                string guess = Console.ReadLine();
                if (scripture.CheckGuess(guess))
                {
                    Console.WriteLine("You guessed correctly!");
                }
                else
                {
                    Console.WriteLine("You guessed incorrectly. Better luck next time");
                }
            }
            else
            {
                scripture.HideRandomWords();
            }

            if (scripture.AreAllWordsHidden())
            {
                Console.WriteLine("All of the words have been hidden. Congratulations!");
                break;
            }
        }
    }
}

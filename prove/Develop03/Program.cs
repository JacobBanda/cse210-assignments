using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    public static void Main(string[] args)
    {
        Scripture scripture = new Scripture("Proverbs 3:5-6", "5 Trust in the Lord with all thine heart; and lean not unto thine own understanding. 6 In all thy ways acknowledge him, and he shall direct thy paths.");
        while (true)
        {
            scripture.Display();

            Console.WriteLine("Press Enter to continue or type 'quit' to exit.");
            string input = Console.ReadLine().Trim().ToLower();

            if (input == "quit")
                break;

            if (!scripture.HideRandom())
                break;
        }
    }
}


public class Word
{
    public string Text;
    public bool IsHidden;

    public Word(string text)
    {
        Text = text;
        IsHidden = false;
    }
}

public class Reference
{
    public string Book;
    public int Chapter;
    public int StartVerse;
    public int EndVerse;

    public Reference(string reference)
    {
        string[] parts = reference.Split(' ');

        string[] bookParts = parts[1].Split(':');
        Book = parts[0];

        string[] verseParts = bookParts[1].Split('-');
        Chapter = int.Parse(bookParts[0]);
        StartVerse = int.Parse(verseParts[0]);

        if (verseParts.Length >= 2)
            EndVerse = int.Parse(verseParts[1]);
        else
            EndVerse = StartVerse;
    }
}

public class Scripture
{
    private readonly List<Word> words;
    private int hiddenCount;

    public Scripture(string reference, string text)
    {
        Reference = new Reference(reference);
        words = text.Split(' ').Select(word => new Word(word)).ToList();
        hiddenCount = 0;
    }

    public Reference Reference { get; }

    public bool HideRandom()
    {
        List<Word> wordsToHide = words.Where(word => !word.IsHidden).ToList();
        if (wordsToHide.Count == 0)
            return false;

        Random random = new Random();
        int index = random.Next(0, wordsToHide.Count);
        wordsToHide[index].IsHidden = true;
        hiddenCount++;

        return true;
    }

    public void Display()
    {
        Console.Clear();
        Console.WriteLine($"{Reference.Book} {Reference.Chapter}:{Reference.StartVerse}-{Reference.EndVerse}");
        foreach (Word word in words)
        {
            string displayWord = word.IsHidden ? "_____" : word.Text;
            Console.Write($"{displayWord} ");
        }
        Console.WriteLine();
    }

    public bool AllWordsHidden()
    {
        return hiddenCount == words.Count;
    }
}
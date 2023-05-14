using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static List<Entry> journal = new List<Entry>();
    static List<string> prompts = new List<string> {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?",
        "What is something I learned today?"
    };
    static void Main(string[] args)
    {
        int choice;
        while (true)
        {
            
            Console.WriteLine("Please select one of the following choices:");
            Console.WriteLine("1. Write");
            Console.WriteLine("2. Display");
            Console.WriteLine("3. Load");
            Console.WriteLine("4. Save");
            Console.WriteLine("5. Quit");
            Console.Write("What would you like to do?");

            choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                
                case 1:
                    AddNewEntry();
                    break;
                case 2:
                    DisplayJournal();
                    break;
                case 3:
                    LoadJournalFromFile();
                    break;
                case 4:
                    SaveJournalToFile();
                    break;
                case 5:
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
    static void AddNewEntry()
    {
        Random rand = new Random();
        int index = rand.Next(prompts.Count);
        string prompt = prompts[index];

        Console.WriteLine($"Prompt: {prompt}");
        string response = Console.ReadLine();
        Entry entry = new Entry(response, prompt, DateTime.Now);
        journal.Add(entry);
    }

    static void DisplayJournal()
    {
        Console.WriteLine("Journal Entries:");
        foreach (Entry entry in journal)
        {
            Console.WriteLine(entry.ToString());
        }
        Console.WriteLine(" ");
    }

    static void SaveJournalToFile()
    {
        Console.Write("Enter a filename to save to: ");
        string filename = Console.ReadLine();

        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (Entry entry in journal)
            {
                string line = $"{entry.Date.ToString("yyyy-MM-dd  HH:mm:ss")}|{entry.Prompt}|{entry.Response}";
                writer.WriteLine(line);
            }
        }

        Console.WriteLine("Journal saved to file.");
    }
     static void LoadJournalFromFile()
    {
        Console.WriteLine("Enter the filename to load the journal:");
        string filename = Console.ReadLine();

        List<Entry> loadedJournal = new List<Entry>();

        using (StreamReader reader = new StreamReader(filename))
        {
            while (!reader.EndOfStream)
            {                
                string line = reader.ReadLine();
                string[] fields = line.Split('|');
                string response = fields[2];
                string prompt = fields[1];
                DateTime date = DateTime.Parse(fields[0]);
                Entry entry = new Entry(response, prompt, date);
                loadedJournal.Add(entry);
            }
        }

        journal = loadedJournal;
        Console.WriteLine("Journal loaded from file.");
    }
}

class Entry
{
    public string Response { get; set; }
    public string Prompt { get; set; }
    public DateTime Date { get; set; }

    public Entry(string response, string prompt, DateTime date)
    {
        Response = response;
        Prompt = prompt;
        Date = date;
    }

    public override string ToString()
    {
        return $"{Date.ToString("yyyy-MM-dd HH:mm:ss")} - {Prompt}: {Response}";
    }
}
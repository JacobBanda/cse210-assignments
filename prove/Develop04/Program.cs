using System;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        bool exit = false;
        while (!exit)
        {
            Console.Clear();
            Console.WriteLine("Welcome to Mindfulness Activities!");
            Console.WriteLine("Please choose an activity:");
            Console.WriteLine("1. Breathing");
            Console.WriteLine("2. Reflection");
            Console.WriteLine("3. Listing");
            Console.WriteLine("4. Exit");

            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    BreathingActivity breathingActivity = new BreathingActivity();
                    breathingActivity.Start();
                    break;
                case "2":
                    ReflectionActivity reflectionActivity = new ReflectionActivity();
                    reflectionActivity.Start();
                    break;
                case "3":
                    ListingActivity listingActivity = new ListingActivity();
                    listingActivity.Start();
                    break;
                case "4":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            Console.WriteLine();

        }
    }
}

abstract class MindfulnessActivity
{
    protected int duration;

    protected void ShowStartingMessage(string activityName, string description)
    {
        Console.WriteLine(activityName);
        Console.WriteLine(description);
        Console.Write("Enter the duration in seconds: ");
        duration = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine();
        Console.WriteLine("Get Ready...");
        Thread.Sleep(3000);
    }

    protected void ShowEndingMessage(string activityName)
    {
        Console.WriteLine();
        Console.WriteLine("Great job! You have completed the " + activityName + ".");
        Console.WriteLine("Duration: " + duration + " seconds");
        Thread.Sleep(5000);
    }
}

public class Spinner
{
    private int counter;

    public void MoveNext()
    {
        counter = (counter + 1) % 4;
    }

    public string Current()
    {
        return counter switch
        {
            0 => "/",
            1 => "-",
            2 => "\\",
            _ => "|",
        };
    }
}

class BreathingActivity : MindfulnessActivity
{

    public void Start()
    {
        ShowStartingMessage("Breathing", "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.");
        PerformBreathingExercise();
        ShowEndingMessage("Breathing");
    }

    private void PerformBreathingExercise()
    {
        ShowSpinner(2);
        for (int i = 0; i < duration; i += 2)
        {
            BreatheIn();
            BreatheOut();
        }
    }

    private void BreatheIn()
    {
        Console.WriteLine("Breathe in...");
        ShowCountdown();
    }

    private void BreatheOut()
    {
        Console.WriteLine("Breathe out...");
        ShowCountdown();
    }

    private void ShowCountdown()
    {
        for (int i = 3; i > 0; i--)
        {
            Console.WriteLine("Countdown: " + i);
            Thread.Sleep(1000); // Pause for 1 second
        }
    }

    private void ShowSpinner(int durationInSeconds)
    {
        var spinner = new Spinner();
        var endTime = DateTime.Now.AddSeconds(durationInSeconds);

        while (DateTime.Now < endTime)
        {
            Console.Write($"\r{spinner.Current()}  ");
            spinner.MoveNext();
            Thread.Sleep(100); // Delay to control spinner speed
        }
        Console.Write("\r   \r"); // Clear spinner
    }
}

class ReflectionActivity : MindfulnessActivity


{
    private string[] prompts = {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private string[] questions = {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?"
    };

    public void Start()
    {
        ShowStartingMessage("Reflection", "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.");
        Random random = new Random();
        int count = 0;

        Console.WriteLine("Think about the following prompt:");
        Console.WriteLine(prompts[random.Next(prompts.Length)]);
        Thread.Sleep(3000);
        Console.WriteLine("You may begin listing...");

        DateTime endTime = DateTime.Now.AddSeconds(duration);
        while (DateTime.Now < endTime)
        {
            Console.WriteLine("Item " + (count + 1) + ": ");
            count++;
            Thread.Sleep(1000);
        }

        Console.WriteLine();
        Console.WriteLine("Number of items listed: " + count);

        ShowEndingMessage("Listing");
    }
}



class ListingActivity : MindfulnessActivity
{
    private string[] prompts = {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    public void Start()
    {
        ShowStartingMessage("Listing", "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.");

        Random random = new Random();
        int count = 0;

        Console.WriteLine("Think about the following prompt:");
        Console.WriteLine(prompts[random.Next(prompts.Length)]);
        Thread.Sleep(3000);
        Console.WriteLine("You may begin listing...");

        DateTime endTime = DateTime.Now.AddSeconds(duration);
        while (DateTime.Now < endTime)
        {
            Console.WriteLine("Item " + (count + 1) + ": ");
            count++;
            Thread.Sleep(1000);
        }

        Console.WriteLine();
        Console.WriteLine("Number of items listed: " + count);

        ShowEndingMessage("Listing");
    }
}
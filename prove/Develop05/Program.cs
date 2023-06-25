using System;
using System.Collections.Generic;

public class Program
{
    private static List<Goal> goals = new List<Goal>();
    private static int score = 0;

    public static void Main(string[] args)
    {
        //LoadGoals();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("You have " + score + " points");
            Console.WriteLine("");
            Console.WriteLine("Menu Options:");
            Console.WriteLine("1. Create New Goal");
            Console.WriteLine("2. List Goals");
            Console.WriteLine("3. Save Goals");
            Console.WriteLine("4. Load Goals");
            Console.WriteLine("5. Record Event");
            Console.WriteLine("6. Exit");
            Console.WriteLine("=============================");

            Console.Write("Enter your choice (1-6): ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    CreateGoal();
                    //ViewGoals();
                    break;
                case "2":
                    ViewGoals();
                    break;
                case "3":
                    SaveGoals();
                    break;
                case "4":
                    LoadGoals();
                    break;
                case "5":
                    RecordProgress();
                    break;
                case "6":                   
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }

private static void LoadGoals()
{
    try
    {
        goals.Clear();
        Console.Write("What is the file name from the Goal File? ");
        string filename = Console.ReadLine();
        
        using (StreamReader reader = new StreamReader(filename))
        {

            // Read the initial score value
            if (!reader.EndOfStream)
            {
                string initialScoreLine = reader.ReadLine();
                if (int.TryParse(initialScoreLine, out int initialScore))
                {
                    score = initialScore;
                }
            }

            while (!reader.EndOfStream)
            {
                string goalLine = reader.ReadLine();
                string[] goalData = goalLine.Split(',');
                string goalType = goalData[0];
                string name = goalData[1];
                string desc = goalData[2];
                int value = int.Parse(goalData[3]);
                bool isComplete = bool.Parse(goalData[4]);

                Goal goal;

                switch (goalType)
                {
                    case "SimpleGoal":
                        goal = new SimpleGoal(name, desc, value);
                        break;
                    case "EternalGoal":
                        goal = new EternalGoal(name, desc, value);
                        break;
                    case "ChecklistGoal":
                        int completionCount = int.Parse(goalData[5]);
                        int targetCount = int.Parse(goalData[6]);
                        int bonusPoints = int.Parse(goalData[7]);
                        goal = new ChecklistGoal(name, desc, value, targetCount, bonusPoints);
                        ((ChecklistGoal)goal).completionCount = completionCount;
                        break;
                    default:
                        Console.WriteLine($"Invalid goal type found: {goalType}");
                        continue;
                }

                goal.IsComplete = isComplete;
                goals.Add(goal);
            }
        }

        Console.WriteLine("Goals loaded from file.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred while loading goals: {ex.Message}");
    }
}

    //private static void SaveGoals()
    //{
        // Save goals to a file or database
        // For simplicity, we won't implement this part in this example
    //    Console.WriteLine("Goals saved.");
    //}

    private static void SaveGoals()
{
    try
    {
        Console.Write("What is the file name for the Goal File? ");
        string filename = Console.ReadLine();
        using (StreamWriter writer = new StreamWriter(filename))
        {
            writer.WriteLine($"{score}");
            foreach (Goal goal in goals)
            {
                if (goal is ChecklistGoal checklistGoal)
                {
                    writer.WriteLine($"{goal.GetType().Name},{goal.Name},{goal.Desc},{goal.Value},{goal.IsComplete},{checklistGoal.completionCount},{checklistGoal.targetCount},{checklistGoal.bonusPoints}");
                }
                else{
                    writer.WriteLine($"{goal.GetType().Name},{goal.Name},{goal.Desc},{goal.Value},{goal.IsComplete}");
                }


                //writer.WriteLine($"{goal.GetType().Name},{goal.Name},{goal.Desc},{goal.Value},{goal.IsComplete}");

                //if (goal is ChecklistGoal checklistGoal)
                //{
                //    writer.WriteLine($"{checklistGoal.completionCount},{checklistGoal.targetCount},{checklistGoal.bonusPoints}");
                //}
            }
        }

        Console.WriteLine("Goals saved to file.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred while saving goals: {ex.Message}");
    }
}

    private static void ViewGoals()
    {
        Console.Clear();
        Console.WriteLine("======= Goals");

        if (goals.Count == 0)
        {
            Console.WriteLine("No goals found.");
        }
        else
        {
            for (int i = 0; i < goals.Count; i++)
            {
                Goal goal = goals[i];
                Console.WriteLine($"{i + 1}. {goal.GetStatus()}");
                //Console.WriteLine($"{i + 1}. {goal.GetStatus()} {goal.Name} ({goal.Desc})");
            }
        }

        Console.WriteLine("======================");
    }

    private static void RecordProgress()
    {
        Console.Clear();
        Console.WriteLine("======= Record Progress");

        if (goals.Count == 0)
        {
            Console.WriteLine("No goals found.");
        }
        else
        {
            ViewGoals();

            Console.Write("Enter the goal number to record progress: ");
            if (int.TryParse(Console.ReadLine(), out int goalNumber) && goalNumber >= 1 && goalNumber <= goals.Count)
            {
                Goal goal = goals[goalNumber - 1];
                
                if (goal.IsComplete)
                {
                    Console.WriteLine("Goal is already complete.");
                }
                else
                {
                    goal.Complete();
                    score += goal.Value;
                    Console.WriteLine($"You gained {goal.Value} points!");
                }
            }
            else
            {
                Console.WriteLine("Invalid goal number.");
            }

             // Update score by summing values of completed goals
            //score = 0;
            //foreach (Goal goal in goals)
            //{
            //    if (goal.IsComplete)
            //    {
            //        score += goal.Value;
            //    }
            //}
        }
    }

    private static void CreateGoal()
    {
        Console.Clear();
        Console.WriteLine("======= Create New Goal");

        Console.Write("Select goal type (1 = Simple, 2 = Eternal, 3 = Checklist): ");
        string typeInput = Console.ReadLine();

        Console.Write("What is the name of your goal? ");
        string name = Console.ReadLine();

        Console.Write("What is a short description of it? ");
        string desc = Console.ReadLine();

        if (int.TryParse(typeInput, out int type) && type >= 1 && type <= 3)
        {
            Console.Write("Enter the goal value: ");
            if (int.TryParse(Console.ReadLine(), out int value))
            {
                switch (type)
                {
                    case 1:
                        goals.Add(new SimpleGoal(name, desc, value));
                        Console.WriteLine("Simple goal created.");
                        break;
                    case 2:
                        goals.Add(new EternalGoal(name, desc, value));
                        Console.WriteLine("Eternal goal created.");
                        break;
                    case 3:
                        Console.Write("How many times does this goal need to be accomplished for a bonus? ");
                        if (int.TryParse(Console.ReadLine(), out int targetCount))
                        {
                            Console.Write("What is the bonus for accomplishing it that many time? ");
                            if (int.TryParse(Console.ReadLine(), out int bonusPoints))
                            {
                                goals.Add(new ChecklistGoal(name, desc, value, targetCount, bonusPoints));
                                Console.WriteLine("Checklist goal created.");
                            }
                            else
                            {
                                Console.WriteLine("Invalid bonus points. Checklist goal creation failed.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid target count. Checklist goal creation failed.");
                        }
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid goal value. Goal creation failed.");
            }
        }
        else
        {
            Console.WriteLine("Invalid goal type. Goal creation failed.");
        }

        Console.WriteLine("================================");
    }
}

// Base class
public abstract class Goal
{
    public string Name { get; protected set; }
    public string Desc { get; protected set; }
    public int Value { get; protected set; }
    public bool IsComplete { get; set; }

    public Goal(string name, string desc, int value)
    {
        Name = name;
        Desc = desc;
        Value = value;
        IsComplete = false;
    }

    public abstract void Complete();
    public abstract string GetStatus();
}


public class SimpleGoal : Goal
{
    public SimpleGoal(string name, string desc, int value) : base(name, desc, value)
    {
    }

    public override void Complete()
    {
        if (!IsComplete)
        {
            IsComplete = true;
            Console.WriteLine($"Completed goal: {Name}");
        }
        else
        {
            Console.WriteLine($"Goal '{Name}' is already completed.");
        }
    }

    public override string GetStatus()
    {
        //return IsComplete ? "[X]" : "[ ]";
        string status = IsComplete ? "[X]" : "[ ]";
        status += $"{Name} ({Desc})";

        return status;
    }

}


public class EternalGoal : Goal
{
    public EternalGoal(string name, string desc, int value) : base(name, desc, value)
    {
    }

    public override void Complete()
    {
        Console.WriteLine($"Recorded progress for eternal goal: {Name}");
    }

    public override string GetStatus()
    {
        //return "[ ]";

        string status = "[ ]";
        status += $"{Name} ({Desc})";

        return status;
    }
}


public class ChecklistGoal : Goal
{
    public int completionCount;
    public int targetCount;
    public int bonusPoints;

    public ChecklistGoal(string name, string desc, int value, int targetCount, int bonusPoints) : base(name, desc, value)
    {
        this.targetCount = targetCount;
        this.bonusPoints = bonusPoints;
    }

    public override void Complete()
    {
        completionCount++;
        Console.WriteLine($"Completed {completionCount}/{targetCount} for checklist goal: {Name}");

        if (completionCount >= targetCount)
        {
            IsComplete = true;
            Console.WriteLine($"Congratulations! Checklist goal '{Name}' is complete!");
        }
    }

    public override string GetStatus()
    {
        string status = IsComplete ? "[X]" : "[ ]";
        status += $"{Name} ({Desc}) -- Currently completed: {completionCount}/{targetCount}";

        return status;
    }
}
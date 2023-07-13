using System;
using System.Collections.Generic;

    public class Program
    {
        public static void Main(string[] args)
        {
            List<Activity> activities = new List<Activity>();


            activities.Add(new Running(new DateTime(2022, 11, 3), 30, 4.8));
            activities.Add(new Cycling(new DateTime(2022, 11, 3), 30, 6.0));
            activities.Add(new Swimming(new DateTime(2022, 11, 3), 30, 30));

            foreach (var activity in activities)
            {
                Console.WriteLine(activity.GetSummary());
            }
        }
    }
    public class Activity
    {
        private DateTime date;
        protected int length;

        public Activity(DateTime date, int length)
        {
            this.date = date;
            this.length = length;
        }

        public virtual double GetDistance()
        {
            return 0;
        }

        public virtual double GetSpeed()
        {
            return 0;
        }

        public virtual double GetPace()
        {
            return 0;
        }

        public string GetSummary()
        {
            string summary = $"{date.ToString("dd MMM yyyy")} {GetType().Name} ({length} min) - ";
            summary += $"Distance: {GetDistance()} miles, ";
            summary += $"Speed: {GetSpeed()} mph, ";
            summary += $"Pace: {GetPace()} min per mile";
            return summary;
        }
    }

    public class Running : Activity
    {
        private double distance;

        public Running(DateTime date, int length, double distance) : base(date, length)
        {
            this.distance = distance;
        }

        public override double GetDistance()
        {
            return distance;
        }

        public override double GetSpeed()
        {
            return (distance / length) * 60;
        }

        public override double GetPace()
        {
            return length / distance;
        }
    }
    public class Cycling : Activity
    {
        private double speed;

        public Cycling(DateTime date, int length, double speed) : base(date, length)
        {
            this.speed = speed;
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

        public Swimming(DateTime date, int length, int laps) : base(date, length)
        {
            this.laps = laps;
        }

        public override double GetDistance()
        {
            return laps * 50 / 1000 * 0.62;
        }

        public override double GetSpeed()
        {
            return (GetDistance() / length) * 60; 
        }

        public override double GetPace()
        {
            return length / GetDistance();
        }
    }

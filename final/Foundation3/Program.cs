using System;
using System.Collections.Generic;

// Program class
public class Program
{
    public static void Main(string[] args)
    {
        Address lectureAddress = new Address("123 Int blvd", "Mcallen", "Texas", "USA");
        Lecture lecture = new Lecture("Institutes of Religion", "Join us for an informative lesson about Institute", DateTime.Now.Date, DateTime.Now, lectureAddress, "Jake Smith", 100);

        Address receptionAddress = new Address("456 Los Angeles St", "Reynosa", "Tamaulipas", "Mexico");
        Reception reception = new Reception("Learn English", "Learn new english words", DateTime.Now.Date, DateTime.Now, receptionAddress, "info@gmail.com");

        Address outdoorGatheringAddress = new Address("789 Beach Rd", "San Diego", "California", "USA");
        OutdoorGathering outdoorGathering = new OutdoorGathering("Beach Party", "Enjoy a day at the beach", DateTime.Now.Date, DateTime.Now, outdoorGatheringAddress, "Sunny");

        Console.WriteLine("--- Event Marketing Messages ---");
        Console.WriteLine();
        Console.WriteLine("Lecture Details:");
        Console.WriteLine(lecture.GetStandardDetails());
        Console.WriteLine();
        Console.WriteLine("Lecture Full Details:");
        Console.WriteLine(lecture.GetFullDetails());
        Console.WriteLine();
        Console.WriteLine("Lecture Short Description:");
        Console.WriteLine(lecture.GetShortDescription());
        Console.WriteLine();
        Console.WriteLine("Reception Details:");
        Console.WriteLine(reception.GetStandardDetails());
        Console.WriteLine();
        Console.WriteLine("Reception Full Details:");
        Console.WriteLine(reception.GetFullDetails());
        Console.WriteLine();
        Console.WriteLine("Reception Short Description:");
        Console.WriteLine(reception.GetShortDescription());
        Console.WriteLine();
        Console.WriteLine("Outdoor Gathering Details:");
        Console.WriteLine(outdoorGathering.GetStandardDetails());
        Console.WriteLine();
        Console.WriteLine("Outdoor Gathering Full Details:");
        Console.WriteLine(outdoorGathering.GetFullDetails());
        Console.WriteLine();
        Console.WriteLine("Outdoor Gathering Short Description:");
        Console.WriteLine(outdoorGathering.GetShortDescription());
    }
}

public class Address
{
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }

    public Address(string street, string city, string state, string country)
    {
        Street = street;
        City = city;
        State = state;
        Country = country;
    }
}

public class Event
{
    private string title;
    private string description;
    private DateTime date;
    private DateTime time;
    private Address address;

    public Event(string title, string description, DateTime date, DateTime time, Address address)
    {
        this.title = title;
        this.description = description;
        this.date = date;
        this.time = time;
        this.address = address;
    }

    public virtual string GetStandardDetails()
    {
        return $"Event: {title}\nDescription: {description}\nDate: {date.ToShortDateString()}\nTime: {time.ToShortTimeString()}\nAddress: {address.Street}, {address.City}, {address.State}, {address.Country}";
    }

    public virtual string GetFullDetails()
    {
        return GetStandardDetails();
    }

    public virtual string GetShortDescription()
    {
        return $"Type: Generic Event\nTitle: {title}\nDate: {date.ToShortDateString()}";
    }
}

public class Lecture : Event
{
    private string speaker;
    private int capacity;

    public Lecture(string title, string description, DateTime date, DateTime time, Address address, string speaker, int capacity)
        : base(title, description, date, time, address)
    {
        this.speaker = speaker;
        this.capacity = capacity;
    }

    public override string GetFullDetails()
    {
        return $"{base.GetFullDetails()}\nType: Lecture\nSpeaker: {speaker}\nCapacity: {capacity}";
    }
}

public class Reception : Event
{
    private string rsvpEmail;

    public Reception(string title, string description, DateTime date, DateTime time, Address address, string rsvpEmail)
        : base(title, description, date, time, address)
    {
        this.rsvpEmail = rsvpEmail;
    }

    public override string GetFullDetails()
    {
        return $"{base.GetFullDetails()}\nType: Reception\nRSVP Email: {rsvpEmail}";
    }
}

public class OutdoorGathering : Event
{
    private string weatherForecast;

    public OutdoorGathering(string title, string description, DateTime date, DateTime time, Address address, string weatherForecast)
        : base(title, description, date, time, address)
    {
        this.weatherForecast = weatherForecast;
    }

    public override string GetFullDetails()
    {
        return $"{base.GetFullDetails()}\nType: Outdoor Gathering\nWeather Forecast: {weatherForecast}";
    }
}


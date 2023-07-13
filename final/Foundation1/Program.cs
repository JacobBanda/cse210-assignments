using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<Video> videos = new List<Video>();

        Video video1 = new Video("Are You Really There?", "The Church of Jesus Christ of Latter-day Saints", 207);
        video1.Comments.Add(new Comment("User 1", "Even when things are at their darkest, there is always hope. Pray, because He really is there and He really does care."));
        video1.Comments.Add(new Comment("User 2", "Even when things are at their darkest, there is always hope. Pray, because He really is there and He really does care."));
        video1.Comments.Add(new Comment("User 3", "Simple and beautiful. I thank you as tears are flowing."));

        Video video2 = new Video("Christlike Service to Refugees", "The Church of Jesus Christ of Latter-day Saints", 495);
        video2.Comments.Add(new Comment("User 1", "Bring them in! What wonderful additions to our community"));
        video2.Comments.Add(new Comment("User 2", "The Church is such a blessing. We are not just a Sunday church. We are active every day."));
        video2.Comments.Add(new Comment("User 3", "This was exactly the inspiration I needed this morning! The Savior's love is boundless, and it is such a blessing to be able to share that love with any and all around us."));

        Video video3 = new Video("The Other Prodigal Son", "The Church of Jesus Christ of Latter-day Saints", 365);
        video3.Comments.Add(new Comment("User 1", "'The green-eyed monster of jealousy'  This parable teaches on how to overcome it. And love others as Christ loves us"));
        video3.Comments.Add(new Comment("User 2", "What a great message to help heal family issues by allowing our hearts to heal!"));
        video3.Comments.Add(new Comment("User 3", "Thank you, Brother Holland, for putting your voice to this wonderful message. Truly a video many can learn from too."));
        video3.Comments.Add(new Comment("User 4", "This is a blessing for me and my family"));


        videos.Add(video1);
        videos.Add(video2);
        videos.Add(video3);

        foreach (var video in videos)
        {
            Console.WriteLine("Title: " + video.Title);
            Console.WriteLine("Author: " + video.Author);
            Console.WriteLine("Length: " + video.Length + " seconds");
            Console.WriteLine("Number of Comments: " + video.GetNumberOfComments());
            Console.WriteLine("Comments:");
            foreach (var comment in video.Comments)
            {
                Console.WriteLine("- " + comment.Name + ": " + comment.Text);
            }
            Console.WriteLine();
        }
    }
}

class Video
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int Length { get; set; }
    public List<Comment> Comments { get; set; }

    public Video(string title, string author, int length)
    {
        Title = title;
        Author = author;
        Length = length;
        Comments = new List<Comment>();
    }

    public int GetNumberOfComments()
    {
        return Comments.Count;
    }
}

class Comment
{
    public string Name { get; set; }
    public string Text { get; set; }

    public Comment(string name, string text)
    {
        Name = name;
        Text = text;
    }
}


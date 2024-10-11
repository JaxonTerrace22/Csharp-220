using System;
using System.Collections.Generic;

class Video
{
    public string Title { get; set; }
    
    
    public string Author { get; set; }
    
    public int LengthInSeconds { get; set; }
    
    public int Views { get; set; }
public int Likes { get; set; }
    public int Dislikes { get; set; }
        public List<Comment> Comments { get; private set; }

    public Video(string title, string author, int lengthInSeconds)
    {
        Title = title;
        
        Author = author;
        
            LengthInSeconds = lengthInSeconds;
        
                Comments = new List<Comment>();
        
            Views = 0;
        
            Likes = 0;
        
            Dislikes = 0;
    }

    public void Play()
    {
        Console.WriteLine("Currently Playing " + Title);
        Views++;
    }

    public void Pause()
    {
        Console.WriteLine("Paused " + Title);
    }

    public void Like()
    {
        Likes++;
    }

    public void Dislike()
    {
        Dislikes++;
    }

    public void AddComment(Comment comment)
    {
        Comments.Add(comment);
    }

    public int CommentCount()
    {
        return Comments.Count;
    }
}

class Comment
{
    public string Username { get; set; }
    public string Text { get; set; }

    public Comment(string username, string text)
    {
        Username = username;
        Text = text;
    }
}

class Playlist
{
    public string Name { get; set; }
    public List<Video> Videos { get; private set; }

    public Playlist(string name)
    {
        Name = name;
        Videos = new List<Video>();
    }

    public void AddVideo(Video video)
    {
        Videos.Add(video);
    }

    public void RemoveVideo(Video video)
    {
        Videos.Remove(video);
    }

    public int VideoCount()
    {
        return Videos.Count;
    }
}

class User
{
    public string Username { get; set; }
    public string Password { get; set; }
    public List<Playlist> Playlists { get; private set; }
    public List<Video> VideoHistory { get; private set; }

    public User(string username, string password)
    {
        Username = username;
        
        Password = password;
        
        Playlists = new List<Playlist>();
        
        VideoHistory = new List<Video>();
    }

    public void Login()
    {
        Console.WriteLine(Username + "is currently logged in.");
    }

    public void Logout()
    {
        Console.WriteLine(Username + "is currently logged out.");
    }

    public Playlist CreatePlaylist(string playlistName)
    {
        var playlist = new Playlist(playlistName);
        
        Playlists.Add(playlist);
        
        return playlist;
    }

    public void DeletePlaylist(Playlist playlist)
    {
        Playlists.Remove(playlist);
    }
}

class Manager
{
    public List<Video> SearchVideos(string keyword, List<Video> videos)
    {
        return videos.FindAll(v => v.Title.Contains(keyword) || v.Author.Contains(keyword));
    }

    public List<Video> SortVideos(List<Video> videos, string sortByCriteria)
    {
        switch (sortByCriteria)
        {
            case "mostViewed":
                videos.Sort((a, b) => b.Views.CompareTo(a.Views));
                break;
            
            case "mostLiked":
                videos.Sort((a, b) => b.Likes.CompareTo(a.Likes));
                break;
        }
        return videos;
    }
}


class Program
{
    static void Main(string[] args)
    {
        
        Video video1 = new Video("Worst movie of 2002", "CINEPHILEPETER", 300);
        
        Video video2 = new Video("Funniest gaming moments this year", "xXgamerguy2002Xx", 500);
        
        Video video3 = new Video("Differential Equations and Linear Algebra made easy", "The CalcuLord Algebro", 600);

        
        video1.AddComment(new Comment("caesar_mclaren42", "xXthis video was not helpful at all big waste of timeXx"));
        
        video1.AddComment(new Comment("catLover54", "Videos like this make me greatful for the internet!"));
        
        video2.AddComment(new Comment("xXProGamerXDXx", "I love how detailed you were lol perfect video <3."));
        
        video3.AddComment(new Comment("TheHoliestKnite2002", "Thanks for your help your video was very educational!"));

        
        List<Video> videos = new List<Video> { video1, video2, video3 };
        
        foreach (var video in videos)
        {
            Console.WriteLine($"Title: {video.Title}, Author: {video.Author}, Length: {video.LengthInSeconds} seconds, Comments: {video.CommentCount()}");
            
            foreach (var comment in video.Comments)
            {
                Console.WriteLine($" - {comment.Username}: {comment.Text}");
            }
        }
    }
}

using BeatSync.Models;
using BeatSync.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace BeatSync.Services;

public class UserService : ObservableObject
{
    private readonly string _artistFilePath = Path.Combine(FileSystem.Current.AppDataDirectory, "Artists.json");
    private readonly string _userFilePath = Path.Combine(FileSystem.Current.AppDataDirectory, "Users.json");
    private readonly string _publisherFilePath = Path.Combine(FileSystem.Current.AppDataDirectory, "Publishers.json");


    public async Task<bool> Authenticate(string username, string password)
    {
        var artist = GetArtists().Where(artist => artist.Username == username && artist.Password == password);
        if(artist.Count() !=  0)
        {
            Application.Current!.MainPage = new PublisherLandingPage();
            return true;
        }

        var publisher = GetPublishers().Where(publisher => publisher.Username == username && publisher.Password == password);
        if (publisher.Count() != 0)
        {
            Application.Current!.MainPage = new PublisherLandingPage();
            return true;
        }

        var user = GetUsers().Where(user => user.Username == username && user.Password == password);
        if (user.Count() != 0)
        {
            Application.Current!.MainPage = new Admin_LandingPage();
            return true;
        }

        return false;
    }

    private ObservableCollection<Artist> GetArtists()
    {
        if (!File.Exists(_artistFilePath))
        {
            return new ObservableCollection<Artist>();
        }

        string json = File.ReadAllText(_artistFilePath);
        var artists = JsonSerializer.Deserialize<ObservableCollection<Artist>>(json);
        return artists!;
    }

    private ObservableCollection<Publisher> GetPublishers()
    {
        if (!File.Exists(_publisherFilePath))
        {
            return new ObservableCollection<Publisher>();
        }

        string json = File.ReadAllText(_publisherFilePath);
        var publishers = JsonSerializer.Deserialize<ObservableCollection<Publisher>>(json);
        return publishers!;
    }

    private ObservableCollection<User> GetUsers()
    {
        if (!File.Exists(_userFilePath))
        {
            return new ObservableCollection<User>();
        }

        string json = File.ReadAllText(_userFilePath);
        var users = JsonSerializer.Deserialize<ObservableCollection<User>>(json);
        return users!;
    }
}

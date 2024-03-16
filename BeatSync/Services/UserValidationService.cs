using BeatSync.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BeatSync.Services;

public class UserValidationService
{
    private readonly string _artistFilePath = Path.Combine(FileSystem.Current.AppDataDirectory, "Artists.json");
    private readonly string _userFilePath = Path.Combine(FileSystem.Current.AppDataDirectory, "Users.json");
    private readonly string _publisherFilePath = Path.Combine(FileSystem.Current.AppDataDirectory, "Publishers.json");

    public bool DoesUsernameExist(string username)
    {
        // Check artists
        var artists = GetArtists();
        if (artists.Any(artist => artist.Username == username))
        {
            return true;
        }

        // Check publishers
        var publishers = GetPublishers();
        if (publishers.Any(publisher => publisher.Username == username))
        {
            return true;
        }

        // Check users
        var users = GetUsers();
        return users.Any(user => user.Username == username);
    }

    public bool DoesEmailAddressExist(string emailAddress)
    {
        // Check artists
        var artists = GetArtists();
        if (artists.Any(artist => artist.Email == emailAddress))
        {
            return true;
        }

        // Check publishers
        var publishers = GetPublishers();
        if (publishers.Any(publisher => publisher.Email == emailAddress))
        {
            return true;
        }

        // Check users
        var users = GetUsers();
        return users.Any(user => user.Email == emailAddress);
    }

    public async Task<bool> DoesUserExist(string identifier)
    {
        return await Task.Run(() =>
        {
            var artists = GetArtists();
            if (artists.Any(artist => artist.Username == identifier || artist.Email == identifier)) 
            {
                return true;
            }

            var publishers = GetPublishers();
            if (publishers.Any(publisher => publisher.Username == identifier || publisher.Email == identifier))
            {
                return true;
            }

            var users = GetUsers();
            return users.Any(user => user.Username == identifier || user.Email == identifier);
        });
    }

 

    public ObservableCollection<Artist> GetArtists()
    {
        if (!File.Exists(_artistFilePath))
        {
            return new ObservableCollection<Artist>();
        }

        string json = File.ReadAllText(_artistFilePath);
        var artists = JsonSerializer.Deserialize<ObservableCollection<Artist>>(json);
        return artists!;
    }

    public ObservableCollection<Publisher> GetPublishers()
    {
        if (!File.Exists(_publisherFilePath))
        {
            return new ObservableCollection<Publisher>();
        }

        string json = File.ReadAllText(_publisherFilePath);
        var publishers = JsonSerializer.Deserialize<ObservableCollection<Publisher>>(json);
        return publishers!;
    }

    public ObservableCollection<User> GetUsers()
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

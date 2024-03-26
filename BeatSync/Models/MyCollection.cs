using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using BeatSync.Services;

namespace BeatSync.Models;

public partial class MyCollection : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Album> _album;

    [ObservableProperty]
    private ObservableCollection<Artist> _artist;

    [ObservableProperty]
    private ObservableCollection<Song> _song;

    [ObservableProperty]
    private ObservableCollection<Publisher> _publisher;

    [ObservableProperty]
    private ObservableCollection<User> _user;

    [ObservableProperty]
    private ObservableCollection<object> _filteredCollection;


    private AdminService _adminService = new();
    private AlbumService _albumService = new();
    private PublisherService _publisherService = new();
    private ArtistService _artistService = new();
    private SongService _songService = new();
    private UserService _userService = new();

    public MyCollection()
    {
        PopulateData();
    }

    public void Filter(string searchQuery)
    {
        FilteredCollection = new ObservableCollection<object>();

        foreach (var album in Album.Where(album => album.Name.Contains(searchQuery)))
            FilteredCollection.Add(album);

        foreach (var artist in Artist.Where(artist => artist.FullName.Contains(searchQuery)))
            FilteredCollection.Add(artist);

        foreach (var artist in Artist.Where(artist => artist.Username.Contains(searchQuery)))
            FilteredCollection.Add(artist);

        foreach (var song in Song.Where(song => song.Name.Contains(searchQuery)))
            FilteredCollection.Add(song);

        foreach (var publisher in Publisher.Where(publisher => publisher.FullName.Contains(searchQuery)))
            FilteredCollection.Add(publisher);

        foreach (var publisher in Publisher.Where(publisher => publisher.Username.Contains(searchQuery)))
            FilteredCollection.Add(publisher);

        foreach (var user in User.Where(user => user.FullName.Contains(searchQuery)))
            FilteredCollection.Add(user);

        foreach (var user in User.Where(user => user.Username.Contains(searchQuery)))
            FilteredCollection.Add(user);
    }



    private async void PopulateData()
    {
        Album = await _albumService.GetActiveAlbumsAsync();
        Artist = await _artistService.GetActiveArtistAsync();
        Song = await _songService.GetActiveSongAsync();
        Publisher = await _publisherService.GetActivePublisherAsync();
        User = await _userService.GetActiveUserAsync();
    }
}
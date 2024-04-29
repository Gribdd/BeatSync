using BeatSync.Services.Service;

namespace BeatSync.Models;

public partial class MyCollection : ObservableObject
{
    private readonly UserService _userService;
    private readonly PublisherService _publisherService;
    private readonly ArtistService _artistService;
    private readonly AlbumService _albumService;
    private readonly SongService _songService;
    private readonly PlaylistService _playlistService;

    [ObservableProperty]
    private ObservableCollection<Album> _album = new();

    [ObservableProperty]
    private ObservableCollection<Artist> _artist = new();

    [ObservableProperty]
    private ObservableCollection<Song> _song = new();

    [ObservableProperty]
    private ObservableCollection<Publisher> _publisher = new();

    [ObservableProperty]
    private ObservableCollection<User> _user = new();

    [ObservableProperty]
    private ObservableCollection<Playlist> _playlist = new();

    [ObservableProperty]
    private ObservableCollection<object> _filteredCollection = new();


    public MyCollection(
        UserService userService,
        PublisherService publisherService,
        ArtistService artistService,
        AlbumService albumService,
        PlaylistService playlistService,
        SongService songService)
    {
        _userService = userService;
        _publisherService = publisherService;
        _artistService = artistService;
        _albumService = albumService;
        _songService = songService;
        _playlistService = playlistService;
        PopulateData();
    }

    public void Filter(string searchQuery)
    {
        if (Album != null)
        {
            foreach (var album in Album.Where(album => album.Name != null && album.Name.Contains(searchQuery)))
                FilteredCollection.Add(album);
        }

        if (Artist != null)
        {
            foreach (var artist in Artist.Where(
                artist => artist.FullName != null && artist.FullName.Contains(searchQuery) ||
                          artist.Username != null && artist.Username.Contains(searchQuery)))
                FilteredCollection.Add(artist);
        }

        if (Song != null)
        {
            foreach (var song in Song.Where(song => song.Name != null && song.Name.Contains(searchQuery)))
                FilteredCollection.Add(song);
        }

        if (Publisher != null)
        {
            foreach (var publisher in Publisher.Where(
                publisher => publisher.FullName != null && publisher.FullName.Contains(searchQuery) ||
                             publisher.Username != null && publisher.Username.Contains(searchQuery)))
                FilteredCollection.Add(publisher);
        }

        if (User != null)
        {
            foreach (var user in User.Where(
                user => user.FullName != null && user.FullName.Contains(searchQuery) ||
                        user.Username != null && user.Username.Contains(searchQuery)))
                FilteredCollection.Add(user);
        }

        if (Playlist != null)
        {
            foreach (var playlist in Playlist.Where(playlist => playlist.Name != null && playlist.Name.Contains(searchQuery)))
                FilteredCollection.Add(playlist);
        }
    }


    private async void PopulateData()
    {
        Album = await _albumService.GetActiveAsync();
        Artist = await _artistService.GetActiveAsync();
        Song = await _songService.GetActiveAsync();
        Publisher = await _publisherService.GetActiveAsync();
        User = await _userService.GetActiveAsync();
        Playlist = await _playlistService.GetActiveAsync();
    }
}
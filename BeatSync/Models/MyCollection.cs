using BeatSync.Services.Service;

namespace BeatSync.Models;

public partial class MyCollection : ObservableObject
{
    private readonly UserService _userService;
    private readonly PublisherService _publisherService;
    private readonly ArtistService _artistService;
    private readonly AlbumService _albumService;
    private readonly SongService _songService;

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
    private ObservableCollection<object> _filteredCollection = new();


    public MyCollection(
        UserService userService,
        PublisherService publisherService,
        ArtistService artistService,
        AlbumService albumService,
        SongService songService)
    {
        _userService = userService;
        _publisherService = publisherService;
        _artistService = artistService;
        _albumService = albumService;
        _songService = songService;
        PopulateData();
    }

    public void Filter(string searchQuery)
    {
        foreach (var album in Album.Where(album => album.Name.Contains(searchQuery)))
            FilteredCollection.Add(album);

        foreach (var artist in Artist.Where(
            artist => artist.FullName.Contains(searchQuery) ||
            artist.Username.Contains(searchQuery)))
            FilteredCollection.Add(artist);

        foreach (var song in Song.Where(song => song.Name.Contains(searchQuery)))
            FilteredCollection.Add(song);

        foreach (var publisher in Publisher.Where(publisher => publisher.FullName.Contains(searchQuery) || publisher.Username.Contains(searchQuery)))
            FilteredCollection.Add(publisher);

        foreach (var user in User.Where(user => user.FullName.Contains(searchQuery) || user.Username.Contains(searchQuery)))
            FilteredCollection.Add(user);
    }

    private async void PopulateData()
    {
        Album = await _albumService.GetActiveAsync();
        Artist = await _artistService.GetActiveAsync();
        Song = await _songService.GetActiveAsync();
        Publisher = await _publisherService.GetActiveAsync();
        User = await _userService.GetActiveAsync();
    }
}
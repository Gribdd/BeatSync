using BeatSync.Services.Service;

namespace BeatSync.ViewModel.ArtistShell;

public partial class ArtistLandingPageViewModel : ObservableObject
{
    private readonly ArtistService _artistService;
    private readonly SongService _songService;
    private readonly AlbumService _albumService;

    [ObservableProperty]
    private ObservableCollection<Song> _song = new();
    [ObservableProperty]
    private ObservableCollection<Album> _album = new();

    private Artist _artist = new();

    public Artist Artist
    {
        get { return _artist; }
        set { SetProperty(ref _artist, value); }
    }

    public ArtistLandingPageViewModel(ArtistService artistService, SongService songService, AlbumService albumService)
    {
        _artistService = artistService;
        _songService = songService;
        _albumService = albumService;
    }

    [RelayCommand]
    async Task OnProfileIconClicked()
    {
        bool answer = await Shell.Current.DisplayAlert("Logout", "Would you like to log out?", "Yes", "No");
        if (answer)
        {
            Application.Current.MainPage = new AppShell();
            Preferences.Default.Set("currentUserId", -1);
        }
    }

    public async Task GetActiveArtist()
    {
        Artist = await _artistService.GetCurrentUser();
    }

    [RelayCommand]
    async Task ViewProfile()
    {
        await GetActiveArtist();
        await Shell.Current.Navigation.PushAsync(new ArtistViewProfile(this));
    }

    [RelayCommand]
    async Task Return()
    {
        // Navigate back
        await Shell.Current.GoToAsync("..");
    }

    public async void GetSongs()
    {
        Song = await _songService.GetSongsByArtistIdAsync(Artist.Id);
    }

    public async Task GetAlbums()
    {
        Album = await _albumService.GetByArtistId(Artist.Id);
    }


}


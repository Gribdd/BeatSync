namespace BeatSync.ViewModel.PublisherShell;

public partial class SongManagementPubViewModel : ObservableObject
{
    private SongService songService;

    [ObservableProperty]
    private MediaSource? _mediaSource;

    [ObservableProperty]
    private ObservableCollection<Song> _songs = new();

    [ObservableProperty]
    private Song _selectedSong = new();

    [ObservableProperty]
    private bool _isVisible;

    public SongManagementPubViewModel(SongService songService)
    {
        this.songService = songService;
    }


    [RelayCommand]
    async Task Logout()
    {
        Shell.Current.FlyoutIsPresented = !Shell.Current.FlyoutIsPresented;
    }

    [RelayCommand]
    async Task AddSong()
    {
        await Shell.Current.GoToAsync($"{nameof(AddSong)}");
    }

    [RelayCommand]
    Task PlaySong(Song song)
    {
        if (song.FilePath == null)
        {
            return Task.CompletedTask;
        }

        SelectedSong = song;
        if (!IsVisible)
        {
            IsVisible = true;
        }

        MediaSource = MediaSource.FromFile(song.FilePath);
        return Task.CompletedTask;
    }

    public async void GetSongsAsync()
    {
        Songs = await songService.GetActiveSongAsync();
    }
}

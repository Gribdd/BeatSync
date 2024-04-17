namespace BeatSync.ViewModel.Users;

public partial class AddPlaylistCustomerViewModel : ObservableObject
{
    private const string Directory = "Playlists";
    private readonly PlaylistService _playlistService;
    private readonly UserService _userService;
    private readonly FileUploadService _fileUploadService;
    private FileResult? fileResultPlaylistImage;

    [ObservableProperty]
    private Playlist _playlist = new();

    [ObservableProperty]
    private User _user = new();

    public AddPlaylistCustomerViewModel(
        PlaylistService playlistService,
        UserService userService,
        FileUploadService fileUploadService)
    {
        _playlistService = playlistService;
        _userService = userService;
        _fileUploadService = fileUploadService;
    }

    [RelayCommand]
    async Task Logout()
    {
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    async Task AddPlaylist()
    {
        Playlist.UserId = User.Id;
        Playlist.SongCount = 0;
        var (isValid, message) = Playlist.IsValid();
        if (!isValid)
        {
            await Shell.Current.DisplayAlert("Add Playlist", message, "OK");
            return;
        }
        await _playlistService.AddAsync(Playlist);
        File.Copy(fileResultPlaylistImage!.FullPath, Playlist.ImageFilePath!);
        await Shell.Current.DisplayAlert("Add Playlist", "Playlist successfully added", "OK");
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    async Task UploadImage()
    {
        (fileResultPlaylistImage, Playlist.ImageFilePath) = await _fileUploadService.UploadImage(Playlist.Name, Directory);
    }

    public async void LoadCurrentUser()
    {
        User = await _userService.GetCurrentUser();
    }
}

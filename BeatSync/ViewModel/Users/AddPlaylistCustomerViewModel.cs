using BeatSync.Services.Service;

namespace BeatSync.ViewModel.Users;

public partial class AddPlaylistCustomerViewModel : ObservableObject
{
    private const string Directory = "Playlists";
    private readonly PlaylistService playlistService;
    private readonly UserService userService;
    private readonly FileUploadService fileUploadService;
    private FileResult? fileResultPlaylistImage;

    [ObservableProperty]
    private Playlist _playlist = new();

    [ObservableProperty]
    private User _user = new();

    public AddPlaylistCustomerViewModel(PlaylistService playlistService, UserService userService, FileUploadService fileUploadService)
    {
        this.playlistService = playlistService;
        this.userService = userService;
        this.fileUploadService = fileUploadService;
    }

    [RelayCommand]
    async Task Logout()
    {
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    async Task AddPlaylist()
    {

        if (User == null)
        {
            await Shell.Current.DisplayAlert("Error", "Something went wrong,", "OK");
            return;
        }

        if (string.IsNullOrEmpty(Playlist.Name))
        {
            await Shell.Current.DisplayAlert("Add Playlist", "Please enter a playlist name first.", "OK");
            return;
        }

        Playlist.UserId = User.Id;
        if (await playlistService.AddPlaylist(Playlist))
        {
            File.Copy(fileResultPlaylistImage!.FullPath, Playlist.ImageFilePath!);
            await Shell.Current.DisplayAlert("Add Playlist", "Playlist successfully added", "OK");
            await Shell.Current.GoToAsync("..");
        }
        else
        {
            await Shell.Current.DisplayAlert("Add Playlist", "Please enter all fields", "OK");
        }
    }

    [RelayCommand]
    async Task UploadImage()
    {
        if (string.IsNullOrEmpty(Playlist.Name))
        {
            await Shell.Current.DisplayAlert("Upload picture", "Please enter song name first", "OK");
            return;
        }

        (fileResultPlaylistImage, Playlist.ImageFilePath) = await fileUploadService.UploadImage(Playlist.Name, Directory);
    }

    public async void LoadCurrentUser()
    {
        User = await userService.GetCurrentUser();
    }
}

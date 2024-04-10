
namespace BeatSync.ViewModel.Admin;

public partial class SongManagementViewModel : ObservableObject
{
    private SongService songService;

    [ObservableProperty]
    private ObservableCollection<Song> _songs = new();

    [ObservableProperty]
    private ObservableCollection<String> _artistName = new();

    public SongManagementViewModel(SongService songService)
    {
        this.songService = songService;
    }

    [RelayCommand]
    async Task NavigateAddSong()
    {
        await Shell.Current.GoToAsync($"{nameof(AddSong)}");
    }

    [RelayCommand]
    async Task DeleteSong()
    {
        string inputId = await Shell.Current.DisplayPromptAsync("Delete Song", "Enter Song ID to delete:");
        if (!string.IsNullOrEmpty(inputId) && int.TryParse(inputId, out int id))
        {
            Songs = await songService.DeleteSongAsync(id);
        }
    }

    [RelayCommand]
    async Task UpdateSong()
    {
        string inputId = await Shell.Current.DisplayPromptAsync("Update Song", "Enter Song ID to delete:");
        if (!string.IsNullOrEmpty(inputId) && int.TryParse(inputId, out int id))
        {

            Songs = await songService.UpdateSongAsync(id);
        }
    }

    [RelayCommand]
    async Task Logout()
    {
    }

    public async void GetSongs()
    {
        Songs = await songService.GetActiveSongAsync();
    }
}

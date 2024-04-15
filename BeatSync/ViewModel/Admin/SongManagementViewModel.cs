
using BeatSync.Services.Service;

namespace BeatSync.ViewModel.Admin;

public partial class SongManagementViewModel : ObservableObject
{
    private readonly SongService _songService;

    [ObservableProperty]
    private ObservableCollection<Song> _songs = new();

    [ObservableProperty]
    private ObservableCollection<String> _artistName = new();

    public SongManagementViewModel(SongService songService)
    {
        _songService = songService;
    }

    [RelayCommand]
    async Task NavigateAddSong()
    {
        await Shell.Current.GoToAsync($"{nameof(AddSong)}");
    }

    [RelayCommand]
    async Task DeleteSong()
    {
        string songName = await Shell.Current.DisplayPromptAsync("Delete Song", "Enter Song name to delete:");
        if (!string.IsNullOrEmpty(songName))
        {
            var song = await _songService.GetByNameAsync(songName);
            await _songService.DeleteAsync(song.Id);
        }
        Songs = await _songService.GetActiveAsync();
    }

    [RelayCommand]
    async Task UpdateSong()
    {
        string songName = await Shell.Current.DisplayPromptAsync("Update Song", "Enter Song name to update:");
        if (!string.IsNullOrEmpty(songName))
        {
            var song = await _songService.GetByNameAsync(songName);
            await _songService.UpdateAsync(song.Id);
        }
        Songs = await _songService.GetActiveAsync();
    }

    [RelayCommand]
    async Task Logout()
    {
    }

    public async void GetSongs()
    {
        Songs = await _songService.GetActiveAsync();
    }
}

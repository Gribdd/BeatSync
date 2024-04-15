
using BeatSync.Services.Service;

namespace BeatSync.ViewModel.Admin;

public partial class ArtistManagementViewModel : ObservableObject
{
    private readonly ArtistService _artistService;

    [ObservableProperty]
    private ObservableCollection<Artist> _artists = new();

    public ArtistManagementViewModel( ArtistService artistService)
    {
        _artistService = artistService;
    }

    [RelayCommand]
    async Task NavigateAddArtist()
    {
        await Shell.Current.GoToAsync($"{nameof(AddArtist)}");
    }

    [RelayCommand]
    async Task DeleteArtist()
    {
        string username = await Shell.Current.DisplayPromptAsync("Delete Artist", "Enter Artist username to delete:");
        if (!string.IsNullOrEmpty(username))
        {
            var artist = await _artistService.GetByUsernameAsync(username);
            await _artistService.DeleteAsync(artist.Id);
        }
        Artists = await _artistService.GetActiveAsync();

    }

    [RelayCommand]
    async Task UpdateArtist()
    {
        string username = await Shell.Current.DisplayPromptAsync("Update Artist", "Enter Artist username to update:");
        if (!string.IsNullOrEmpty(username))
        {
            var artist = await _artistService.GetByUsernameAsync(username);
            await _artistService.UpdateAsync(artist.Id);
        }
        Artists = await _artistService.GetActiveAsync();
    }

    [RelayCommand]
    async Task Logout()
    {

    }

    public async void GetArtists()
    {
        Artists = await _artistService.GetActiveAsync();
    }
}


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
        string inputId = await Shell.Current.DisplayPromptAsync("Delete Artist", "Enter Artist ID to delete:");
        if (!string.IsNullOrEmpty(inputId) && int.TryParse(inputId, out int id))
        {
            await _artistService.DeleteAsync(id);
        }
        Artists = await _artistService.GetActiveAsync();

    }

    [RelayCommand]
    async Task UpdateArtist()
    {
        string inputId = await Shell.Current.DisplayPromptAsync("Update Artist", "Enter Artist ID to delete:");
        if (!string.IsNullOrEmpty(inputId) && int.TryParse(inputId, out int id))
        {

            await _artistService.UpdateAsync(id);
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

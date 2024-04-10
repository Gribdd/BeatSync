
namespace BeatSync.ViewModel.Admin;

public partial class ArtistManagementViewModel : ObservableObject
{
    private ArtistService artistService;

    [ObservableProperty]
    private ObservableCollection<Artist> _artists = new();

    public ArtistManagementViewModel( ArtistService artistService)
    {
        this.artistService = artistService;
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
            Artists = await artistService.DeleteArtistAsync(id);
        }

    }

    [RelayCommand]
    async Task UpdateArtist()
    {
        string inputId = await Shell.Current.DisplayPromptAsync("Update Artist", "Enter Artist ID to delete:");
        if (!string.IsNullOrEmpty(inputId) && int.TryParse(inputId, out int id))
        {

            Artists = await artistService.UpdateArtistAsync(id);
        }
    }

    [RelayCommand]
    async Task Logout()
    {

    }

    public async void GetArtists()
    {
        Artists = await artistService.GetActiveArtistAsync();
    }
}

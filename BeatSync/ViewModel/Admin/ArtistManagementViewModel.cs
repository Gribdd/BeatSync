using BeatSync.Models;
using BeatSync.Pages;
using BeatSync.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;


namespace BeatSync.ViewModel.Admin;

public partial class ArtistManagementViewModel : ObservableObject
{
    private AdminService _adminService;

    [ObservableProperty]
    private ObservableCollection<Artist> _artists = new();

    public ArtistManagementViewModel(AdminService adminService)
    {
        _adminService = adminService;
    }

    [RelayCommand]
    async void NavigateAddArtist()
    {
        await Shell.Current.GoToAsync($"{nameof(AddArtist)}");
    }

    [RelayCommand]
    async void DeleteArtist()
    {
        string inputId = await Shell.Current.DisplayPromptAsync("Delete Artist", "Enter Artist ID to delete:");
        if (!string.IsNullOrEmpty(inputId) && int.TryParse(inputId, out int id))
        {
            Artists = await _adminService.DeleteArtistAsync(id);
        }

    }

    [RelayCommand]
    async void UpdateArtist()
    {
        string inputId = await Shell.Current.DisplayPromptAsync("Update Artist", "Enter Artist ID to delete:");
        if (!string.IsNullOrEmpty(inputId) && int.TryParse(inputId, out int id))
        {

            Artists = await _adminService.UpdateArtistAsync(id);
        }
    }

    public ICommand GetArtistsCommand => new Command(GetArtists);

    public async void GetArtists()
    {
        Artists = await _adminService.GetActiveArtistAsync();
    }
}

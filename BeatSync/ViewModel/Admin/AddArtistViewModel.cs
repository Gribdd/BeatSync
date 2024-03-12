using BeatSync.Models;
using BeatSync.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BeatSync.ViewModel.Admin;

public partial class AddArtistViewModel : ObservableObject
{

    private AdminService _adminService;

    [ObservableProperty]
    private Artist _artist = new();

    public AddArtistViewModel(AdminService adminService)
    {
        _adminService = adminService;
    }

    [RelayCommand]
    async void AddArtist()
    {
        if (await _adminService.AddArtistAsync(Artist))
        {
            await Shell.Current.DisplayAlert("Add Artist", "Artist successfully added", "OK");
            await Shell.Current.GoToAsync("..");
        }
        else
        {
            await Shell.Current.DisplayAlert("Add Artist", "Please enter all fields", "OK");
        }
    }

    [RelayCommand]
    async void Return()
    {
        await Shell.Current.GoToAsync("..");
    }
}

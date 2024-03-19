using BeatSync.Pages;
using BeatSync.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BeatSync.ViewModel.PublisherShell;

public partial class LibraryPageViewModel : ObservableObject
{
    private AdminService? _adminService;

    public LibraryPageViewModel(AdminService adminService)
    {
        _adminService = adminService;
    }


    [RelayCommand]
    async Task AddAlbum()
    {
        await Shell.Current.GoToAsync($"{nameof(AddAlbumPublisher)}");
    }

    [RelayCommand]
    async Task Logout()
    {
        await _adminService!.Logout();
    }
}

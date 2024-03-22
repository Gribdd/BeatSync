using BeatSync.Models;
using BeatSync.Pages;
using BeatSync.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace BeatSync.ViewModel.PublisherShell;

public partial class LibraryPageViewModel : ObservableObject
{
    private AdminService? _adminService;

    [ObservableProperty]
    private ObservableCollection<Album> _albums = new();

    [ObservableProperty]
    private Album _selectedAlbum = new();

    public LibraryPageViewModel(AdminService? adminService)
    {
        _adminService = adminService;
    }


    [RelayCommand]
    async Task AddAlbum()
    {
        await Shell.Current.GoToAsync($"{nameof(AddAlbumPublisher)}");
    }

    [RelayCommand]
    async Task NavigateToAddAlbumSongs(Album album)
    {
        var navigationParameter = new Dictionary<string, object>
        {
            {nameof(Album), album }
        };
        await Shell.Current.GoToAsync($"{nameof(AddAlbumSongs)}",navigationParameter);
    }

    [RelayCommand]
    async Task Logout()
    {
        await _adminService!.Logout();
    }

    public async void GetAlbums()
    {
        Albums = await _adminService!.GetAlbumsAsync();
    }
}

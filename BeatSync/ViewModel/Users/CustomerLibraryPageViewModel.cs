using BeatSync.Models;
using BeatSync.Pages;
using BeatSync.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
namespace BeatSync.ViewModel.Users;

public partial class CustomerLibraryPageViewModel : ObservableObject
{
    private readonly AdminService adminService;
    private readonly UserService userService;
    private readonly PlaylistService playlistService;

    [ObservableProperty]
    private User _user = new();

    [ObservableProperty]
    private ObservableCollection<Playlist> _playlists = new();


    public CustomerLibraryPageViewModel(AdminService adminService, UserService userService, PlaylistService playlistService)
    {
        this.adminService = adminService;
        this.userService = userService;
        this.playlistService = playlistService;
    }


    [RelayCommand]
    async Task Logout()
    {
        await adminService.Logout();
    }

    [RelayCommand]
    async Task NavigateAddPlaylist()
    {
        await Shell.Current.GoToAsync($"{nameof(AddPlaylistCustomer)}");
    }

    [RelayCommand]
    async Task NavigateAddPlaylistSongs()
    {
        await Shell.Current.GoToAsync($"{nameof(AddPlaylistSongsCustomer)}");
    }

    public async Task LoadCurrentUser()
    {
        User = await userService.GetCurrentUser();
    }

    public async void LoadPlaylists()
    {
        Playlists = await playlistService.GetPlaylistsByUserAsync(User.Id);
    }

}

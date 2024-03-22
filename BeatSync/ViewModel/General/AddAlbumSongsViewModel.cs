using BeatSync.Models;
using BeatSync.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;


namespace BeatSync.ViewModel.General;

//first parameter receives the data
//second parameter is the parameter id specied in the shell route
[QueryProperty(nameof(Album), nameof(Album))]
public partial class AddAlbumSongsViewModel : ObservableObject
{
    private AdminService _adminService;

    [ObservableProperty]
    private Album _album = new();

    [ObservableProperty]
    private ObservableCollection<Song> _songs = new();


    public AddAlbumSongsViewModel(AdminService adminService)
    {
        _adminService = adminService;
    }

    public async void GetSongsByArtistId()
    {
        Songs = await _adminService.GetSongsByArtistIdAsync(Album.ArtistId);
    }

    [RelayCommand]
    async Task Return()
    {
        await Shell.Current.GoToAsync("..");
    }
}

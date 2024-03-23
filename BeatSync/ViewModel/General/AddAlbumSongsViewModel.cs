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

    [RelayCommand]
    async Task AddSongToAlbum()
    {
        string[] songNames = Songs.Select(s => s.Name!).ToArray();
        string selectedSongName = await Shell.Current.DisplayActionSheet("Add song to album", "Cancel", null, songNames);

        if(selectedSongName == null)
        {
            return;
        }
        
        Song? selectedSong = Songs.FirstOrDefault(song => string.Equals(selectedSongName, song.Name));
        // Initialize the album's songs collection if it's null
        Album.Songs ??= new();

        Album.Songs!.Add(selectedSong!);
        Album.Songs = await _adminService.AddAlbumSongAsync(Album);
    }
}

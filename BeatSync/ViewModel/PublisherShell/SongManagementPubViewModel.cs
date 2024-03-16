using BeatSync.Models;
using BeatSync.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Plugin.Maui.Audio;
using System.Collections.ObjectModel;
using System.Diagnostics;


namespace BeatSync.ViewModel.PublisherShell;

public partial class SongManagementPubViewModel : ObservableObject
{
    private AdminService _adminService;
    readonly IAudioManager audioManager;

    [ObservableProperty]
    private ObservableCollection<Song> _songs = new();


    public SongManagementPubViewModel(AdminService adminService, IAudioManager audioManager)
    {
        _adminService = adminService;
        this.audioManager = audioManager;
    }


    [RelayCommand]
    async Task Logout()
    {
        await _adminService.Logout();
    }

    [RelayCommand]
    async Task AddSong()
    {
        await Shell.Current.GoToAsync($"{nameof(AddSong)}");
    }

    [RelayCommand]
    async Task PlaySong(Song song)
    {
        if(song.FilePath == null)
        {
            return;
        }

        await FileSystem.OpenAppPackageFileAsync("test");
    }

    public async void GetSongsAsync()
    {
        Songs = await _adminService.GetActiveSongAsync();
    }
}

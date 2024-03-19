using BeatSync.Models;
using BeatSync.Services;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Plugin.Maui.Audio;
using System.Collections.ObjectModel;
using System.Diagnostics;


namespace BeatSync.ViewModel.PublisherShell;

public partial class SongManagementPubViewModel : ObservableObject
{
    private AdminService _adminService;

    [ObservableProperty]
    private MediaSource? _mediaSource;

    [ObservableProperty]
    private ObservableCollection<Song> _songs = new();


    public SongManagementPubViewModel(AdminService adminService)
    {
        _adminService = adminService;
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
    Task PlaySong(Song song)
    {
        if (song.FilePath == null)
        {
            return Task.CompletedTask;
        }

        MediaSource = MediaSource.FromFile(song.FilePath);
        return Task.CompletedTask;
    }

    public async void GetSongsAsync()
    {
        Songs = await _adminService.GetActiveSongAsync();
    }
}

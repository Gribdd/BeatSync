using BeatSync.Models;
using BeatSync.Pages;
using BeatSync.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BeatSync.ViewModel.Admin;

public partial class SongManagementViewModel : ObservableObject
{
    private AdminService _adminService;

    [ObservableProperty]
    private ObservableCollection<Song> _songs = new();

    public SongManagementViewModel(AdminService adminService)
    {
        _adminService = adminService;
    }

    [RelayCommand]
    async Task NavigateAddSong()
    {
        await Shell.Current.GoToAsync($"{nameof(AddSong)}");
    }

    [RelayCommand]
    async Task DeleteSong()
    {
        string inputId = await Shell.Current.DisplayPromptAsync("Delete Song", "Enter Song ID to delete:");
        if (!string.IsNullOrEmpty(inputId) && int.TryParse(inputId, out int id))
        {
            Songs = await _adminService.DeleteSongAsync(id);
        }
    }

    [RelayCommand]
    async Task UpdateSong()
    {
        string inputId = await Shell.Current.DisplayPromptAsync("Update Song", "Enter Song ID to delete:");
        if (!string.IsNullOrEmpty(inputId) && int.TryParse(inputId, out int id))
        {

            Songs = await _adminService.UpdateSongAsync(id);
        }
    }

    public ICommand GetSongsCommand => new Command(GetSongs);

    public async void GetSongs()
    {
        Songs = await _adminService.GetActiveSongAsync();
    }
}

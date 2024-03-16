using BeatSync.Models;
using BeatSync.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatSync.ViewModel.PublisherShell;

public partial class SongManagementPubViewModel : ObservableObject
{
    private AdminService _adminService;

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

    public async void GetSongsAsync()
    {
        Songs = await _adminService.GetActiveSongAsync();
    }
}

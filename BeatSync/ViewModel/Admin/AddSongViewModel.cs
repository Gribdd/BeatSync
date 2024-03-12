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

namespace BeatSync.ViewModel.Admin;

public partial class AddSongViewModel : ObservableObject
{
    private AdminService _adminService;

    [ObservableProperty]
    private Song _song = new();

    [ObservableProperty]
    private Artist _selectedArtist = new();

    [ObservableProperty]
    private ObservableCollection<Artist> _artists = new();

    public string[] Genres { get; set; } = { "Rap", "Pop", "Indie", "OPM", "Punk Rock" };
    public AddSongViewModel(AdminService adminService)
    {
        _adminService = adminService;
    }

    [RelayCommand]
    async Task AddSong()
    {
        Song.ArtistID = SelectedArtist.Id;
        if (await _adminService.AddSongAsync(Song))
        {
            await Shell.Current.DisplayAlert("Add Song", "Song successfully added", "OK");
            await Shell.Current.GoToAsync("..");
        }
        else
        {
            await Shell.Current.DisplayAlert("Add Song", "Please enter all fields", "OK");
        }
    }

    [RelayCommand]
    async Task Return()
    {
        await Shell.Current.GoToAsync("..");
    }

    public async Task PopulateArtist()
    {
        Artists = await _adminService.GetActiveArtistAsync();
    }
}

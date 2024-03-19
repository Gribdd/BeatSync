using BeatSync.Models;
using BeatSync.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatSync.ViewModel.Admin;

public partial class AddAlbumViewModel : ObservableObject
{
    private AdminService _adminService;

    [ObservableProperty]
    private Album _album = new();

    public AddAlbumViewModel(AdminService adminService)
    {
        _adminService = adminService;
    }

    [RelayCommand]
    async Task Return()
    {
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    async Task AddAlbum()
    {
        if (await _adminService.AddAlbumAsync(Album))
        {
            await Shell.Current.DisplayAlert("Add Album", "Album successfully added", "OK");
            await Shell.Current.GoToAsync("..");
        }
        else
        {
            await Shell.Current.DisplayAlert("Add Album", "Please enter all fields", "OK");
        }
    }


}

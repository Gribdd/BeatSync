using BeatSync.Models;
using BeatSync.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}

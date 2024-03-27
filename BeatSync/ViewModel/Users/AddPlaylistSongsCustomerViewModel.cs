using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatSync.ViewModel.Users;

public partial class AddPlaylistSongsCustomerViewModel : ObservableObject
{
    [RelayCommand]
    async Task Logout()
    {
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    async Task AddAlbumSongs()
    {
        await Shell.Current.DisplayAlert("Test","Test","OK");
    }
}

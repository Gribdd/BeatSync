using BeatSync.Models;
using BeatSync.Pages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatSync.ViewModel.LoginAndRegistration;

[QueryProperty(nameof(User), nameof(User))]
public partial class CreateAccountPasswordViewModel : ObservableObject
{
    [ObservableProperty]
    private User _user = new();

    [RelayCommand]
    async Task Return()
    {
        await Shell.Current.GoToAsync($"..");
    }

    [RelayCommand]
    async Task NavigateToCreatePassword()
    {
        await Shell.Current.GoToAsync($"{nameof(CreateAccountDOB)}?User={User}");
    }
}

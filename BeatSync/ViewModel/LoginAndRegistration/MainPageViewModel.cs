using BeatSync.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatSync.ViewModel.LoginAndRegistration;

public partial class MainPageViewModel : ObservableObject
{
    [RelayCommand]
    async Task SignUpCustomer()
    {
        await Shell.Current.GoToAsync($"{nameof(SignUpPage)}");
    }

    [RelayCommand]
    async Task SignUpPublisher()
    {
        await Shell.Current.GoToAsync($"{nameof(SignUpPage)}");
    }

    [RelayCommand]
    async Task SignUpArtist()
    {
        await Shell.Current.GoToAsync($"{nameof(SignUpPage)}");
    }

    [RelayCommand]
    async Task Login()
    {
        await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
    }

    [RelayCommand]
    async Task LoginAdmin()
    {
        await Shell.Current.GoToAsync($"{nameof(Admin_LoginPage)}");
    }
}

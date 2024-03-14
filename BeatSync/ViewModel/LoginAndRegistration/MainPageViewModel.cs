using BeatSync.Models;
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
    [ObservableProperty]
    private User _user = new();
   
    [RelayCommand]
    async Task SignUpCustomer()
    {
        User.AccounType = 3;
        var navigationParameter = new Dictionary<string, object>
        {
            {nameof(User), User }
        };
        await Shell.Current.GoToAsync("signup",navigationParameter);
    }

    [RelayCommand]
    async Task SignUpPublisher()
    {
        User.AccounType = 2;
        var navigationParameter = new Dictionary<string, object>
        {
            {nameof(User), User }
        };
        await Shell.Current.GoToAsync("signup", navigationParameter);
    }

    [RelayCommand]
    async Task SignUpArtist()
    {
        User.AccounType = 1;
        var navigationParameter = new Dictionary<string, object>
        {
            {nameof(User), User }
        };
        await Shell.Current.GoToAsync("signup", navigationParameter);
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

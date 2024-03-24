using BeatSync.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatSync.ViewModel.LoginAndRegistration;

public partial class LoginPageViewModel : ObservableObject
{
    private readonly UserAuthService _userSevice;
    
    [ObservableProperty]
    private object _user = new();

    [ObservableProperty]
    private string? _username;

    [ObservableProperty]
    private string? _password;

    public LoginPageViewModel(UserAuthService userService)
    {
        _userSevice = userService;    
    }


    [RelayCommand]
    async Task NavigateToSignIn()
    {
        await Shell.Current.GoToAsync($"..");
    }

    [RelayCommand]
    async Task Authenticate()
    {
        if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
        {
            await Shell.Current.DisplayAlert("Error", "Please fill in all fields","OK");
            return;
        }

        bool isAuthenticated = await _userSevice.Authenticate(Username, Password);
        if(!isAuthenticated)
        {
            await Shell.Current.DisplayAlert("Error", "User not found", "OK");
            Username = Password = string.Empty;
        }
    }
}

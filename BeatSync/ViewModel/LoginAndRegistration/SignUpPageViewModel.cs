using BeatSync.Models;
using BeatSync.Pages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using BeatSync.Services;

namespace BeatSync.ViewModel.LoginAndRegistration;

[QueryProperty(nameof(User), nameof(User))]
public partial class SignUpPageViewModel : ObservableObject
{
    private UserValidationService _userValidationService;

    [ObservableProperty]
    private User _user = new();

    public SignUpPageViewModel(UserValidationService userValidationService)
    {
        _userValidationService = userValidationService;
    }


    [RelayCommand]
    async Task Return()
    {
        await Shell.Current.GoToAsync($"..");
    }

    [RelayCommand]
    async Task NavigateToCreateUsername()
    {
        if (string.IsNullOrEmpty(User.Email))
        {
            await Shell.Current.DisplayAlert("Oops!", "You must enter an email address.", "Ok");
            return;
        }

        if (!IsEmailValid(User.Email))
        {
            await Shell.Current.DisplayAlert("Oops!", "You must enter a valid email address.", "Ok");
            return;
        }

        if (_userValidationService.DoesEmailAddressExist(User.Email))
        {
            await Shell.Current.DisplayAlert("Oops!", "This email address is already in use.", "Ok");
            return;
        }

        var navigationParameter = new Dictionary<string, object>
        {
            {nameof(User), User }
        };

        await Shell.Current.GoToAsync("createaccountusername", navigationParameter);
    }

    private bool IsEmailValid(string email)
    {
        if (email != null)
        {
            string pattern = @"^[\w\.-]+@[\w\.-]+\.\w+$";
            return Regex.IsMatch(email, pattern);
        }
        else
        {
            return false;
        }
    }
}

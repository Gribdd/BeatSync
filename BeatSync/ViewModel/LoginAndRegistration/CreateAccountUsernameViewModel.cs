using BeatSync.Models;
using BeatSync.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatSync.ViewModel.LoginAndRegistration;

[QueryProperty(nameof(User), nameof(User))]

public partial class CreateAccountUsernameViewModel : ObservableObject
{

    private UserValidationService _userValidationService;

	[ObservableProperty]
	private User _user = new();

    public CreateAccountUsernameViewModel(UserValidationService userValidationService)
    {
        _userValidationService = userValidationService;     
    }


    [RelayCommand]
	async Task Return()
	{
        await Shell.Current.GoToAsync("..");
    }

	[RelayCommand]
	async Task NavigateToCreatePassword()
	{
		if (string.IsNullOrEmpty(User.Username))
		{
            await Shell.Current.DisplayAlert("Oops!", "We need a username to proceed.", "Ok");
			return;
        }

        if (_userValidationService.DoesUsernameExist(User.Username))
        {
            await Shell.Current.DisplayAlert("Oops!", "This username already exists. Please try using another one.", "Ok");
            return;
        }

        var navigationParameter = new Dictionary<string, object>
		{
			{nameof(User), User }
        };


        await Shell.Current.GoToAsync("createaccountpassword", navigationParameter);
    }
}
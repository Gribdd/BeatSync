using BeatSync.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;


namespace BeatSync.ViewModel.LoginAndRegistration;

[QueryProperty(nameof(User), nameof(User))]
public partial class CreateAccountPasswordViewModel : ObservableObject
{
    [ObservableProperty]
    private User _user = new();

    [RelayCommand]
    async Task Return()
    {
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    async Task NavigateToCreateDOB()
    {
        if (string.IsNullOrEmpty(User.Password))
        {
            await Shell.Current.DisplayAlert("Oops!", "We need a password to proceed.", "Ok");
            return;
        }

        User.DateOfBirth = DateTime.Now;
        var navigationParameter = new Dictionary<string, object>
        {
            {nameof(User), User }
        };
        await Shell.Current.GoToAsync("createaccountdob", navigationParameter);
    }
}

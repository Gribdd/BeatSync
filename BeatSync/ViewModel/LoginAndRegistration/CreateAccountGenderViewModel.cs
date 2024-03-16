using BeatSync.Models;
using BeatSync.Pages;
using BeatSync.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;


namespace BeatSync.ViewModel.LoginAndRegistration;

[QueryProperty(nameof(User), nameof(User))]
public partial class CreateAccountGenderViewModel : ObservableObject
{
    private AdminService _adminService;
    [ObservableProperty]
    private User _user = new();

    public CreateAccountGenderViewModel(AdminService adminService)
    {
        _adminService = adminService;
    }

    [RelayCommand]
    async Task Return()
    {
        await Shell.Current.GoToAsync("..");
    }


    [RelayCommand]
    async Task NavigateToCreateUploadImage()
    {
        if (string.IsNullOrWhiteSpace(User.Gender))
        {
            await Shell.Current.DisplayAlert("Oops!", "You must specify your gender.", "Ok");
            return;
        }

        var navigationParameter = new Dictionary<string, object>
        {
            {nameof(User), User }
        };
        await Shell.Current.GoToAsync("createaccountuploadimage", navigationParameter);
    }
}

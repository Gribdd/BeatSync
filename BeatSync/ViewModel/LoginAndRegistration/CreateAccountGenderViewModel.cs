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

    /* moved to CreateAccountUploadImageViewModel
    async Task NavigateToLandingPage()
    {
        User.IsDeleted = false;
        switch (User.AccounType)
        {
            //artist
            case 1:
                Artist artist = new()
                {
                    AccounType = User.AccounType,
                    Email = User.Email,
                    Password = User.Password,
                    DateOfBirth = User.DateOfBirth,
                    FirstName = User.FirstName,
                    LastName = User.LastName,
                    Gender = User.Gender
                };

                if(await _adminService.AddArtistAsync(artist))
                {
                    await Shell.Current.DisplayAlert("Add Artist", "Artist successfully added", "OK");
                    await Shell.Current.GoToAsync("mainpage");
                }

                break;
            //publisher
            case 2:
                Publisher publisher = new()
                {
                    AccounType = User.AccounType,
                    Email = User.Email,
                    Password = User.Password,
                    DateOfBirth = User.DateOfBirth,
                    FirstName = User.FirstName,
                    LastName = User.LastName,
                    Gender = User.Gender
                };

                if (await _adminService.AddPublisherAsync(publisher))
                {
                    await Shell.Current.DisplayAlert("Add Publisher", "Publisher successfully added", "OK");
                    await Shell.Current.GoToAsync("mainpage");
                }
                break;
            //customer
            case 3:
                if (await _adminService.AddUserAsync(User))
                {
                    await Shell.Current.DisplayAlert("Add User", "User successfully added", "OK");
                    Application.Current.MainPage = new LandingPage();
                }
                break;
            default:
                break;

        }
    }*/

}

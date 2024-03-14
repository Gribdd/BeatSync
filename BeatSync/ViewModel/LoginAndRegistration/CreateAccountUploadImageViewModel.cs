using BeatSync.Models;
using BeatSync.Pages;
using BeatSync.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Storage;

namespace BeatSync.ViewModel.LoginAndRegistration;

[QueryProperty(nameof(User), nameof(User))]
public partial class CreateAccountUploadImageViewModel : ObservableObject
{
    private AdminService _adminService;
    private FileResult? _fileResult;

    [ObservableProperty]
	private User _user = new();

    public CreateAccountUploadImageViewModel(AdminService adminService)
    {
        _adminService = adminService;
    }

	[RelayCommand]
	async Task Return()
	{
        await Shell.Current.GoToAsync("..");
    }

	[RelayCommand]
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

                if (await _adminService.AddArtistAsync(artist))
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
    }

    [RelayCommand]
    async Task UploadImage()
    {
        if (string.IsNullOrEmpty(User.FirstName) || string.IsNullOrEmpty(User.FirstName))
        {
            await Shell.Current.DisplayAlert("Upload picture", "Please enter user first and last name first", "OK");
            return;
        }

        _fileResult = await FilePicker.PickAsync(new PickOptions
        {
            PickerTitle = "Please pick an image for the user",
            FileTypes = FilePickerFileType.Images
        });

        if (_fileResult == null)
        {
            return;
        }

        //make dir 
        string dir = Path.Combine(FileSystem.Current.AppDataDirectory, "Users");
        _adminService.CreateDirectoryIfMissing(dir);

        User.ImageFilePath = Path.Combine(dir, $"{User.FirstName + User.LastName}.jpg");
        await Shell.Current.DisplayAlert("Upload picture", "Picture successfully uploaded ", "OK");
    }
}
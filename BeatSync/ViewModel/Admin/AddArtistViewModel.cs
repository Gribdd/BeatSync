using BeatSync.Models;
using BeatSync.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Text.RegularExpressions;


namespace BeatSync.ViewModel.Admin;

public partial class AddArtistViewModel : ObservableObject
{
    private const string Directory = "Artists";
    private AdminService adminService;
    private ArtistService artistService;
    private FileUploadService fileUploadService;
    private UserValidationService userValidationService;
    private FileResult? fileResult;

    [ObservableProperty]
    private Artist _artist = new();

    public AddArtistViewModel(AdminService adminService, UserValidationService userValidationService, ArtistService artistService, FileUploadService fileUploadService)
    {
        this.adminService = adminService;
        this.userValidationService = userValidationService;
        this.artistService = artistService;
        this.fileUploadService = fileUploadService;
    }

    [RelayCommand]
    async Task AddArtist()
    {
        bool isValidArtist = await ValidateFields();
        if (!isValidArtist)
        {
            return;
        }

        if (await artistService.AddArtistAsync(Artist))
        {
            File.Copy(fileResult!.FullPath, Artist.ImageFilePath!);
            await Shell.Current.DisplayAlert("Add Artist", "Artist successfully added", "OK");
            await Shell.Current.GoToAsync("..");
        }
        else
        {
            await Shell.Current.DisplayAlert("Add Artist", "Please enter all fields", "OK");
        }
    }

    [RelayCommand]
    async Task Return()
    {
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    async Task UploadImage()
    {
        if (string.IsNullOrEmpty(Artist.FirstName) || string.IsNullOrEmpty(Artist.FirstName))
        {
            await Shell.Current.DisplayAlert("Upload picture", "Please enter artist first and last name first", "OK");
            return;
        }

        (fileResult, Artist.ImageFilePath) = await fileUploadService.UploadImage(Artist.Username, Directory);
    }

    private async Task<bool> ValidateFields()
    {
        if (string.IsNullOrEmpty(Artist.Email) || string.IsNullOrEmpty(Artist.Username) || string.IsNullOrEmpty(Artist.Password) || string.IsNullOrEmpty(Artist.FirstName) || string.IsNullOrEmpty(Artist.LastName) || string.IsNullOrEmpty(Artist.Gender))
        {
            await Shell.Current.DisplayAlert("Oops!", "Please enter all fields", "Ok");
            return false;
        }

        if (!IsEmailValid(Artist.Email))
        {
            await Shell.Current.DisplayAlert("Oops!", "You must enter a valid email address.", "Ok");
            return false;
        }

        if (Artist.DateOfBirth >= DateTime.Now.Date)
        {
            await Shell.Current.DisplayAlert("Error!", "You cannot set your date of birth to today's date.", "Ok");
            return false;
        }

        if (userValidationService.DoesEmailAddressExist(Artist.Email))
        {
            await Shell.Current.DisplayAlert("Oops!", "This email already exists. Please try using another one.", "Ok");
            return false;
        }

        if (userValidationService.DoesUsernameExist(Artist.Username))
        {
            await Shell.Current.DisplayAlert("Oops!", "This username already exists. Please try using another one.", "Ok");
            return false;
        }

        if (string.IsNullOrEmpty(Artist.ImageFilePath))
        {
            await Shell.Current.DisplayAlert("Upload picture", "Please upload artist picture first", "OK");
            return false;
        }
        return true;
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

using BeatSync.Models;
using BeatSync.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Text.RegularExpressions;

namespace BeatSync.ViewModel.Admin;

public partial class AddUserViewModel : ObservableObject
{
    private const string Directory = "Users";
    private AdminService adminService;
    private UserService userService;
    private FileUploadService fileUploadService;
    private UserValidationService userValidationService;
    private FileResult? fileResult;

    [ObservableProperty]
    private User _user = new();

    public AddUserViewModel(AdminService adminService, UserValidationService userValidationService, UserService userService, FileUploadService fileUploadService)
    {
        this.adminService = adminService;
        this.userValidationService = userValidationService;
        this.userService = userService;
        this.fileUploadService = fileUploadService;
    }

    [RelayCommand]
    async Task AddUser()
    {
        bool isValidUser = await ValidateFields();
        if (!isValidUser)
        {
            return;
        }

        if (await userService.AddUserAsync(User))
        {
            File.Copy(fileResult!.FullPath, User.ImageFilePath!);
            await Shell.Current.DisplayAlert("Add User", "User successfully added", "OK");
            await Shell.Current.GoToAsync("..");
        }
        else
        {
            await Shell.Current.DisplayAlert("Add User", "Please enter all fields", "OK");
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
        if (string.IsNullOrEmpty(User.FirstName) || string.IsNullOrEmpty(User.FirstName))
        {
            await Shell.Current.DisplayAlert("Upload picture", "Please enter user firsta and last name first", "OK");
            return;
        }

        (fileResult, User.ImageFilePath) = await fileUploadService.UploadImage(User.Username, Directory);
    }

    private async Task<bool> ValidateFields()
    {
        if (string.IsNullOrEmpty(User.Email) || string.IsNullOrEmpty(User.Username) || string.IsNullOrEmpty(User.Password) || string.IsNullOrEmpty(User.FirstName) || string.IsNullOrEmpty(User.LastName) || string.IsNullOrEmpty(User.Gender))
        {
            await Shell.Current.DisplayAlert("Oops!", "Please enter all fields", "Ok");
            return false;
        }

        if (!IsEmailValid(User.Email))
        {
            await Shell.Current.DisplayAlert("Oops!", "You must enter a valid email address.", "Ok");
            return false;
        }

        if (userValidationService.DoesEmailAddressExist(User.Email))
        {
            await Shell.Current.DisplayAlert("Oops!", "This email already exists. Please try using another one.", "Ok");
            return false;
        }

        if (userValidationService.DoesUsernameExist(User.Username))
        {
            await Shell.Current.DisplayAlert("Oops!", "This username already exists. Please try using another one.", "Ok");
            return false;
        }

        if (User.DateOfBirth >= DateTime.Now.Date)
        {
            await Shell.Current.DisplayAlert("Error!", "You cannot set your date of birth to today's date.", "Ok");
            return false;
        }

        if (string.IsNullOrEmpty(User.ImageFilePath))
        {
            await Shell.Current.DisplayAlert("Upload picture", "Please upload user picture first", "OK");
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

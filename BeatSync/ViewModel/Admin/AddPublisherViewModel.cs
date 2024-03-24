using BeatSync.Models;
using BeatSync.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Text.RegularExpressions;


namespace BeatSync.ViewModel.Admin;

public partial class AddPublisherViewModel : ObservableObject
{
    const string Directory = "Publishers";
    private AdminService adminService;
    private PublisherService publisherService;
    private FileUploadService fileUploadService;
    private UserValidationService userValidationService;
    private FileResult? fileResult;

    [ObservableProperty]
    private Publisher _publisher = new();

    public AddPublisherViewModel(AdminService adminService, UserValidationService userValidationService, PublisherService publisherService, FileUploadService fileUploadService)
    {
        this.adminService = adminService;
        this.userValidationService = userValidationService;
        this.publisherService = publisherService;
        this.fileUploadService = fileUploadService;
    }

    [RelayCommand]
    async Task AddPublisher()
    {
        bool isValidPublisher = await ValidateFields();
        if(!isValidPublisher)
        {
            return;
        }

        if (await publisherService.AddPublisherAsync(Publisher))
        {
            File.Copy(fileResult!.FullPath, Publisher.ImageFilePath!);
            await Shell.Current.DisplayAlert("Add Publisher", "Publisher successfully added", "OK");
            await Shell.Current.GoToAsync("..");
        }
        else
        {
            await Shell.Current.DisplayAlert("Add Publisher", "Please enter all fields", "OK");
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

        if (string.IsNullOrEmpty(Publisher.FirstName) || string.IsNullOrEmpty(Publisher.LastName))
        {
            await Shell.Current.DisplayAlert("Upload picture", "Please enter first and last name first", "OK");
            return;
        }

        (fileResult, Publisher.ImageFilePath) = await fileUploadService.UploadImage(Publisher.Username, Directory);

    }

    private async Task<bool> ValidateFields()
    {
        if (string.IsNullOrEmpty(Publisher.Email) || string.IsNullOrEmpty(Publisher.Username) || string.IsNullOrEmpty(Publisher.Password) || string.IsNullOrEmpty(Publisher.FirstName) || string.IsNullOrEmpty(Publisher.LastName) || string.IsNullOrEmpty(Publisher.Gender))
        {
            await Shell.Current.DisplayAlert("Oops!", "Please enter all fields", "Ok");
            return false;
        }

        if (!IsEmailValid(Publisher.Email))
        {
            await Shell.Current.DisplayAlert("Oops!", "You must enter a valid email address.", "Ok");
            return false;
        }

        if (userValidationService.DoesEmailAddressExist(Publisher.Email))
        {
            await Shell.Current.DisplayAlert("Oops!", "This email already exists. Please try using another one.", "Ok");
            return false;
        }

        if (userValidationService.DoesUsernameExist(Publisher.Username))
        {
            await Shell.Current.DisplayAlert("Oops!", "This username already exists. Please try using another one.", "Ok");
            return false;
        }

        if (Publisher.DateOfBirth >= DateTime.Now.Date)
        {
            await Shell.Current.DisplayAlert("Error!", "You cannot set your date of birth to today's date.", "Ok");
            return false;
        }

        if (string.IsNullOrEmpty(Publisher.ImageFilePath))
        {
            await Shell.Current.DisplayAlert("Upload picture", "Please upload publisher picture first", "OK");
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


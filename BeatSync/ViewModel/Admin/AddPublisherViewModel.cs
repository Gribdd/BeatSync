using BeatSync.Models;
using BeatSync.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Text.RegularExpressions;


namespace BeatSync.ViewModel.Admin;

public partial class AddPublisherViewModel : ObservableObject
{
    private AdminService _adminService;
    private FileResult? _fileResult;

    [ObservableProperty]
    private Publisher _publisher = new();

    public AddPublisherViewModel(AdminService adminService)
    {
        _adminService = adminService;
    }

    [RelayCommand]
    async Task AddPublisher()
    {
        if (string.IsNullOrEmpty(Publisher.Email) || string.IsNullOrEmpty(Publisher.Username) || string.IsNullOrEmpty(Publisher.Password) || string.IsNullOrEmpty(Publisher.FirstName) || string.IsNullOrEmpty(Publisher.LastName) || string.IsNullOrEmpty(Publisher.Gender))
        {
            await Shell.Current.DisplayAlert("Oops!", "Please enter all fields", "Ok");
            return;
        }

        if (!IsEmailValid(Publisher.Email))
        {
            await Shell.Current.DisplayAlert("Oops!", "You must enter a valid email address.", "Ok");
            return;
        }

        if (Publisher.DateOfBirth >= DateTime.Now.Date)
        {
            await Shell.Current.DisplayAlert("Error!", "You cannot set your date of birth to today's date.", "Ok");
            return;
        }

       

        if (string.IsNullOrEmpty(Publisher.ImageFilePath))
        {
            await Shell.Current.DisplayAlert("Upload picture", "Please upload publisher picture first", "OK");
            return;
        }

        if (await _adminService.AddPublisherAsync(Publisher))
        {
            File.Copy(_fileResult!.FullPath, Publisher.ImageFilePath);
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

        _fileResult = await FilePicker.PickAsync(new PickOptions
        {
            PickerTitle = "Please pick an image for the movie",
            FileTypes = FilePickerFileType.Images
        });

        if (_fileResult == null)
        {
            return;
        }

        //make dir 
        string dir = Path.Combine(FileSystem.Current.AppDataDirectory, "Publishers");
        _adminService.CreateDirectoryIfMissing(dir);

        Publisher.ImageFilePath = Path.Combine(dir, $"{Publisher.FirstName+Publisher.LastName}.jpg");
        await Shell.Current.DisplayAlert("Upload picture", "Picture successfully uploaded ", "OK");
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


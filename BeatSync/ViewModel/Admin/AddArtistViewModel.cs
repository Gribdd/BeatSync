using BeatSync.Models;
using BeatSync.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Text.RegularExpressions;


namespace BeatSync.ViewModel.Admin;

public partial class AddArtistViewModel : ObservableObject
{

    private AdminService _adminService;
    private FileResult? _fileResult;

    [ObservableProperty]
    private Artist _artist = new();

    public AddArtistViewModel(AdminService adminService)
    {
        _adminService = adminService;
    }

    [RelayCommand]
    async Task AddArtist()
    {
        if (string.IsNullOrEmpty(Artist.Email) || string.IsNullOrEmpty(Artist.Username) || string.IsNullOrEmpty(Artist.Password) || string.IsNullOrEmpty(Artist.FirstName) || string.IsNullOrEmpty(Artist.LastName) || string.IsNullOrEmpty(Artist.Gender))
        {
            await Shell.Current.DisplayAlert("Oops!", "Please enter all fields", "Ok");
            return;
        }

        if (!IsEmailValid(Artist.Email))
        {
            await Shell.Current.DisplayAlert("Oops!", "You must enter a valid email address.", "Ok");
            return;
        }

        if (Artist.DateOfBirth >= DateTime.Now.Date)
        {
            await Shell.Current.DisplayAlert("Error!", "You cannot set your date of birth to today's date.", "Ok");
            return;
        }

        if (string.IsNullOrEmpty(Artist.ImageFilePath))
        {
            await Shell.Current.DisplayAlert("Upload picture", "Please upload artist picture first", "OK");
            return;
        }


        if (await _adminService.AddArtistAsync(Artist))
        {
            File.Copy(_fileResult!.FullPath, Artist.ImageFilePath);
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
        string dir = Path.Combine(FileSystem.Current.AppDataDirectory, "Artists");
        _adminService.CreateDirectoryIfMissing(dir);

        Artist.ImageFilePath = Path.Combine(dir, $"{Artist.FirstName+Artist.LastName}.jpg");
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

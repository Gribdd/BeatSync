using BeatSync.Models;
using BeatSync.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Text.RegularExpressions;

namespace BeatSync.ViewModel.Admin
{
    public partial class AddUserViewModel : ObservableObject
    {
        private AdminService _adminService;
        private UserValidationService _userValidationService;
        private FileResult? _fileResult;

        [ObservableProperty]
        private User _user = new();

        public AddUserViewModel(AdminService adminService, UserValidationService userValidationService)
        {
            _adminService = adminService;
            _userValidationService = userValidationService;
        }

        [RelayCommand]
        async Task AddUser()
        {
            if (string.IsNullOrEmpty(User.Email) || string.IsNullOrEmpty(User.Username) || string.IsNullOrEmpty(User.Password) || string.IsNullOrEmpty(User.FirstName) || string.IsNullOrEmpty(User.LastName) || string.IsNullOrEmpty(User.Gender))
            {
                await Shell.Current.DisplayAlert("Oops!", "Please enter all fields", "Ok");
                return;
            }

            if (!IsEmailValid(User.Email))
            {
                await Shell.Current.DisplayAlert("Oops!", "You must enter a valid email address.", "Ok");
                return;
            }

            if (_userValidationService.DoesEmailAddressExist(User.Email))
            {
                await Shell.Current.DisplayAlert("Oops!", "This email already exists. Please try using another one.", "Ok");
                return;
            }

            if (_userValidationService.DoesUsernameExist(User.Username))
            {
                await Shell.Current.DisplayAlert("Oops!", "This username already exists. Please try using another one.", "Ok");
                return;
            }

            if (User.DateOfBirth >= DateTime.Now.Date)
            {
                await Shell.Current.DisplayAlert("Error!", "You cannot set your date of birth to today's date.", "Ok");
                return;
            }


            if (string.IsNullOrEmpty(User.ImageFilePath))
            {
                await Shell.Current.DisplayAlert("Upload picture", "Please upload user picture first", "OK");
                return;
            }

            if (await _adminService.AddUserAsync(User))
            {
                File.Copy(_fileResult!.FullPath, User.ImageFilePath);
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
            string dir = Path.Combine(FileSystem.Current.AppDataDirectory, "Users");
            _adminService.CreateDirectoryIfMissing(dir);

            User.ImageFilePath = Path.Combine(dir, $"{User.FirstName + User.LastName}.jpg");
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
}

using BeatSync.Models;
using BeatSync.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BeatSync.ViewModel.Admin
{
    public partial class AddUserViewModel: ObservableObject
    {
        private AdminService _adminService;
        private FileResult? _fileResult;

        [ObservableProperty]
        private User _user = new();

        public AddUserViewModel(AdminService adminService)
        {
            _adminService = adminService;
        }

        [RelayCommand]
        async Task AddUser()
        {
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

            User.ImageFilePath = Path.Combine(dir, $"{User.FirstName+User.LastName}.jpg");
            await Shell.Current.DisplayAlert("Upload picture", "Picture successfully uploaded ", "OK");
        }
    }
}

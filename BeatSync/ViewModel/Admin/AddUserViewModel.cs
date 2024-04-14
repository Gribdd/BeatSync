

namespace BeatSync.ViewModel.Admin;

public partial class AddUserViewModel : ObservableObject
{
    private const string Directory = "Users";
    private FileResult? fileResult;

    private readonly FileUploadService _fileUploadService;
    private readonly UserService _userService;
    private readonly UserValidationService _userValidationService;

    [ObservableProperty]
    private User _user = new();

    public AddUserViewModel(
        UserValidationService userValidationService, 
        FileUploadService fileUploadService,
        UserService userService)
    {
        _userValidationService = userValidationService;
        _fileUploadService = fileUploadService;
        _userService = userService;
    }

    [RelayCommand]
    async Task AddUser()
    {
        var (isValid, message) = User.IsValid();
        if (!isValid)
        {
            await Shell.Current.DisplayAlert("Error!", message, "Ok");
            return;
        }

        if (!await IsNonExistingAccount())
        {
            return;
        }

        await _userService.AddAsync(User);
        File.Copy(fileResult!.FullPath, User.ImageFilePath!);
        await Shell.Current.DisplayAlert("Add User", "User successfully added", "OK");
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    async Task Return()
    {
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    async Task UploadImage()
    {
        (fileResult, User.ImageFilePath) = await _fileUploadService.UploadImage(User.Username, Directory);
    }

    private async Task<bool> IsNonExistingAccount()
    {
        if (_userValidationService.DoesEmailAddressExist(User.Email!))
        {
            await Shell.Current.DisplayAlert("Oops!", "This email already exists. Please try using another one.", "Ok");
            return false;
        }

        if (_userValidationService.DoesUsernameExist(User.Username!))
        {
            await Shell.Current.DisplayAlert("Oops!", "This username already exists. Please try using another one.", "Ok");
            return false;
        }
        return true;
    }
}

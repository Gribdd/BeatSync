
namespace BeatSync.ViewModel.Admin;

public partial class AddArtistViewModel : ObservableObject
{
    private const string Directory = "Artists";
    private readonly ArtistService _artistService;
    private readonly UserValidationService _userValidationService;
    private readonly FileUploadService _fileUploadService;
    private FileResult? fileResult;

    [ObservableProperty]
    private Artist _artist = new();

    public AddArtistViewModel(
        ArtistService artistService,
        UserValidationService userValidationService,
        FileUploadService fileUploadService)
    {
        _artistService = artistService;
        _userValidationService = userValidationService;
        _fileUploadService = fileUploadService;
    }


    [RelayCommand]
    async Task AddArtist()
    {
        var (isValid, message) = Artist.IsValid();
        if (!isValid)
        {
            await Shell.Current.DisplayAlert("Error!", message, "Ok");
            return;
        }

        if (!await IsNonExistingAccount())
        {
            return;
        }

        await _artistService.AddAsync(Artist);
        File.Copy(fileResult!.FullPath, Artist.ImageFilePath!);
        await Shell.Current.DisplayAlert("Add Artist", "Artist successfully added", "OK");
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
        if (string.IsNullOrEmpty(Artist.FirstName) || string.IsNullOrEmpty(Artist.FirstName))
        {
            await Shell.Current.DisplayAlert("Upload picture", "Please enter artist first and last name first", "OK");
            return;
        }

        (fileResult, Artist.ImageFilePath) = await _fileUploadService.UploadImage(Artist.Username, Directory);
    }

    private async Task<bool> IsNonExistingAccount()
    {

        if (_userValidationService.DoesEmailAddressExist(Artist.Email!))
        {
            await Shell.Current.DisplayAlert("Oops!", "This email already exists. Please try using another one.", "Ok");
            return false;
        }

        if (_userValidationService.DoesUsernameExist(Artist.Username!))
        {
            await Shell.Current.DisplayAlert("Oops!", "This username already exists. Please try using another one.", "Ok");
            return false;
        }
        return true;
    }
}


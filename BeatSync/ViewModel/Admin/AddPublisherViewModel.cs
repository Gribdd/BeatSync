
namespace BeatSync.ViewModel.Admin;

public partial class AddPublisherViewModel : ObservableObject
{
    const string Directory = "Publishers";
    private readonly PublisherService _publisherService;
    private readonly FileUploadService _fileUploadService;
    private readonly UserValidationService _userValidationService;
    private FileResult? fileResult;

    [ObservableProperty]
    private Publisher _publisher = new();

    public AddPublisherViewModel(
        UserValidationService userValidationService, 
        PublisherService publisherService, 
        FileUploadService fileUploadService)
    {
        _userValidationService = userValidationService;
        _publisherService = publisherService;
        _fileUploadService = fileUploadService;
    }

    [RelayCommand]
    async Task AddPublisher()
    {
        var (isValid, message) = Publisher.IsValid();
        if (!isValid)
        {
            await Shell.Current.DisplayAlert("Error!", message, "Ok");
            return;
        }

        if (!await IsNonExistingAccount())
        {
            return;
        }

        await _publisherService.AddAsync(Publisher);
        File.Copy(fileResult!.FullPath, Publisher.ImageFilePath!);
        await Shell.Current.DisplayAlert("Add Publisher", "User successfully added", "OK");
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

        if (string.IsNullOrEmpty(Publisher.FirstName) || string.IsNullOrEmpty(Publisher.LastName))
        {
            await Shell.Current.DisplayAlert("Upload picture", "Please enter first and last name first", "OK");
            return;
        }

        (fileResult, Publisher.ImageFilePath) = await _fileUploadService.UploadImage(Publisher.Username, Directory);

    }

    private async Task<bool> IsNonExistingAccount()
    {
        if (_userValidationService.DoesEmailAddressExist(Publisher.Email!))
        {
            await Shell.Current.DisplayAlert("Oops!", "This email already exists. Please try using another one.", "Ok");
            return false;
        }

        if (_userValidationService.DoesUsernameExist(Publisher.Username!))
        {
            await Shell.Current.DisplayAlert("Oops!", "This username already exists. Please try using another one.", "Ok");
            return false;
        }

        return true;
    }
}


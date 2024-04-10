namespace BeatSync.ViewModel.LoginAndRegistration;

[QueryProperty(nameof(User), nameof(User))]
public partial class CreateAccountUploadImageViewModel : ObservableObject
{
    private ArtistService artistService;
    private PublisherService publisherService;
    private UserService userService;
    private FileUploadService fileUploadService;
    private FileResult? fileResult;

    PublisherLandingPageViewModel publisherLandingPageViewModel;
    CustomerLandingPageViewModel customerLandingPageViewModel;

    [ObservableProperty]
    private User _user = new();

    public CreateAccountUploadImageViewModel(
        ArtistService artistService, 
        PublisherService publisherService, 
        UserService userService, 
        FileUploadService fileUploadService, 
        PublisherLandingPageViewModel publisherLandingPageViewModel,
        CustomerLandingPageViewModel customerLandingPageViewModel)
    {
        this.artistService = artistService;
        this.publisherService = publisherService;
        this.userService = userService;
        this.fileUploadService = fileUploadService;
        this.publisherLandingPageViewModel = publisherLandingPageViewModel;
        this.customerLandingPageViewModel = customerLandingPageViewModel;
    }

    [RelayCommand]
    async Task Return()
    {
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    async Task NavigateToLandingPage()
    {
        int activeUserId = -1;
        User.IsDeleted = false;
        switch (User.AccounType)
        {
            //artist
            case 1:
                Artist artist = new()
                {
                    AccounType = User.AccounType,
                    Username = User.Username,
                    Email = User.Email,
                    Password = User.Password,
                    DateOfBirth = User.DateOfBirth,
                    FirstName = User.FirstName,
                    LastName = User.LastName,
                    Gender = User.Gender,
                    ImageFilePath = User.ImageFilePath
                };

                if (await artistService.AddArtistAsync(artist))
                {
                    File.Copy(fileResult!.FullPath, artist.ImageFilePath!);
                    await Shell.Current.DisplayAlert("Add Artist", "Artist successfully added", "OK");
                    //await Shell.Current.GoToAsync("mainpage");
                    Application.Current!.MainPage = new PublisherLandingPage(publisherLandingPageViewModel);
                }

                break;
            //publisher
            case 2:
                Publisher publisher = new()
                {
                    AccounType = User.AccounType,
                    Username = User.Username,
                    Email = User.Email,
                    Password = User.Password,
                    DateOfBirth = User.DateOfBirth,
                    FirstName = User.FirstName,
                    LastName = User.LastName,
                    Gender = User.Gender,
                    ImageFilePath = User.ImageFilePath
                };

                if (await publisherService.AddPublisherAsync(publisher))
                {
                    File.Copy(fileResult!.FullPath, publisher.ImageFilePath!);
                    await Shell.Current.DisplayAlert("Add Publisher", "Publisher successfully added", "OK");
                    Application.Current!.MainPage = new PublisherLandingPage(publisherLandingPageViewModel);
                }
                break;
            //customer
            case 3:
                if (await userService.AddUserAsync(User))
                {
                    File.Copy(fileResult!.FullPath, User.ImageFilePath!);
                    await Shell.Current.DisplayAlert("Add User", "User successfully added", "OK");
                    Application.Current!.MainPage = new CustomerLandingPage(customerLandingPageViewModel);
                }
                break;
            default:
                break;
        }
        Preferences.Default.Set("currentUserId", activeUserId);
    }

    [RelayCommand]
    async Task UploadImage()
    {
        if (string.IsNullOrEmpty(User.FirstName) || string.IsNullOrEmpty(User.FirstName))
        {
            await Shell.Current.DisplayAlert("Upload picture", "Please enter user first and last name first", "OK");
            return;
        }

        string directory = User.AccounType == 1 ? "Artists" : User.AccounType == 2 ? "Publishers" : "Users";
        (fileResult, User.ImageFilePath) = await fileUploadService.UploadImage(User.Username, directory);
    }
}
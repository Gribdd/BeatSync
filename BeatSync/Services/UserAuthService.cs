namespace BeatSync.Services;

public class UserAuthService : ObservableObject
{
    private readonly UserValidationService userValidationService;
    private readonly PublisherLandingPageViewModel publisherLandingPageViewModel;
    private readonly CustomerLandingPageViewModel customerLandingPageViewModel;

    public UserAuthService(
        UserValidationService userValidationService,
        PublisherLandingPageViewModel publisherLandingPageViewModel,
        CustomerLandingPageViewModel customerLandingPageViewModel)
    {
        this.userValidationService = userValidationService;
        this.publisherLandingPageViewModel = publisherLandingPageViewModel;
        this.customerLandingPageViewModel = customerLandingPageViewModel;
    }


    public async Task<bool> Authenticate(string identifier, string password)
    {
        if(!await userValidationService.DoesUserExist(identifier))
        {
            return false;
        }

        var user = await GetUser(identifier, password);

        if(user != null)
        {
            NavigateBasedOnUserType(user);
            return true;
        }
        
        return false;
    }

    private void NavigateBasedOnUserType(Object incomingUser)
    {
        int activeUserId = -1;
        switch (incomingUser)
        {
            //artist
            case Artist artist:
                activeUserId = artist.Id;
                Application.Current!.MainPage = new PublisherLandingPage(publisherLandingPageViewModel);
                break;
            //publisher
            case Publisher publisher:
                activeUserId = publisher.Id;
                Application.Current!.MainPage = new PublisherLandingPage(publisherLandingPageViewModel);
                break;
            case User user:
                Application.Current!.MainPage = new CustomerLandingPage(customerLandingPageViewModel);
                activeUserId = user.Id;
                break;
            default:
                break;
        }
        Preferences.Default.Set("currentUserId", activeUserId);
    }

    private async Task<Object?> GetUser(string identifier, string password)
    {
        var artists =  await userValidationService.GetArtists();
        var artist = artists.FirstOrDefault(a => (a.Username == identifier || a.Email == identifier) && (a.Password == password));
        if (artist != null)
        {
            return artist;
        }

        var publishers = await userValidationService.GetPublishers();
        var publisher = publishers.FirstOrDefault(p => (p.Username == identifier || p.Email == identifier) && (p.Password == password));
        if (publisher != null)
        {
            return publisher;
        }

        var users = await userValidationService.GetUsers();
        var user = users.FirstOrDefault(u => (u.Username == identifier || u.Email == identifier) && (u.Password == password));
        return user;
    }   
}

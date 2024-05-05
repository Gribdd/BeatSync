using BeatSync.ViewModel.ArtistShell;

namespace BeatSync.Services;

public class UserAuthService : ObservableObject
{
    private readonly UserValidationService userValidationService;
    private readonly PublisherLandingPageViewModel publisherLandingPageViewModel;
    private readonly CustomerLandingPageViewModel customerLandingPageViewModel;
    private readonly ArtistLandingPageViewModel artistLandingPageViewModel;

    public UserAuthService(
        UserValidationService userValidationService,
        PublisherLandingPageViewModel publisherLandingPageViewModel,
        CustomerLandingPageViewModel customerLandingPageViewModel,
        ArtistLandingPageViewModel artistLandingPageViewModel)
    {
        this.userValidationService = userValidationService;
        this.publisherLandingPageViewModel = publisherLandingPageViewModel;
        this.customerLandingPageViewModel = customerLandingPageViewModel;
        this.artistLandingPageViewModel = artistLandingPageViewModel;

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
        int activeAccountType = -1;
        switch (incomingUser)
        {
            //artist
            case Artist artist:
                activeUserId = artist.Id;
                activeAccountType = artist.AccountType;
                Application.Current!.MainPage = new ArtistLandingPage(artistLandingPageViewModel);
                break;
            //publisher
            case Publisher publisher:
                activeUserId = publisher.Id;
                activeAccountType = publisher.AccountType;
                Application.Current!.MainPage = new PublisherLandingPage(publisherLandingPageViewModel);
                break;
            case User user:
                Application.Current!.MainPage = new CustomerLandingPage(customerLandingPageViewModel);
                activeUserId = user.Id;
                activeAccountType = user.AccountType;
                break;
            default:
                break;
        }
        Preferences.Default.Set("currentUserId", activeUserId);
        Preferences.Default.Set("currentAccountType", activeAccountType);
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

namespace BeatSync.Services;

public class UserValidationService
{
    private readonly ArtistService artistService;
    private readonly PublisherService publisherService;
    private readonly UserService userService;

    private ObservableCollection<Artist> artists = new();
    private ObservableCollection<Publisher> publishers = new();
    private ObservableCollection<User> users = new();

    public UserValidationService(ArtistService artistService, PublisherService publisherService, UserService userService)
    {
        this.artistService = artistService;
        this.publisherService = publisherService;
        this.userService = userService;
        LoadData();
    }

    //will be consumed by userAuthService
    public async Task<ObservableCollection<Artist>> GetArtists() => await artistService.GetArtistsAsync();
    public async Task<ObservableCollection<Publisher>> GetPublishers() => await publisherService.GetPublishersAsync();
    public async Task<ObservableCollection<User>> GetUsers() => await userService.GetUsersAsync();

    private async void LoadData()
    {
        artists = await GetArtists();
        publishers = await GetPublishers();
        users = await GetUsers();
    }

    public bool DoesUsernameExist(string username)
    {
        // Check artists
        if (artists.Any(artist => artist.Username == username))
        {
            return true;
        }

        // Check publishers
        if (publishers.Any(publisher => publisher.Username == username))
        {
            return true;
        }

        // Check users
        return users.Any(user => user.Username == username);
    }

    public bool DoesEmailAddressExist(string emailAddress)
    {
        // Check artists
        if (artists.Any(artist => artist.Email == emailAddress))
        {
            return true;
        }

        // Check publishers
        if (publishers.Any(publisher => publisher.Email == emailAddress))
        {
            return true;
        }

        // Check users
        return users.Any(user => user.Email == emailAddress);
    }

    public async Task<bool> DoesUserExist(string identifier)
    {
        return await Task.Run(() =>
        {
            if (artists.Any(artist => artist.Username == identifier || artist.Email == identifier)) 
            {
                return true;
            }

            if (publishers.Any(publisher => publisher.Username == identifier || publisher.Email == identifier))
            {
                return true;
            }

            return users.Any(user => user.Username == identifier || user.Email == identifier);
        });
    }
}

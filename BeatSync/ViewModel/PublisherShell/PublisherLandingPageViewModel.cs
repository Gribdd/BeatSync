namespace BeatSync.ViewModel.PublisherShell;

public partial class PublisherLandingPageViewModel : ObservableObject
{
    private readonly PublisherService _publisherService;
    private readonly AlbumService _albumService;

    private Publisher _publisher = new();
    public Publisher Publisher
    {
        get { return _publisher; }
        set { SetProperty(ref _publisher, value); }
    }

    [ObservableProperty]
    private ObservableCollection<Album> _albums = new();    

    public PublisherLandingPageViewModel(PublisherService publisherService, AlbumService albumService)
    {
        _publisherService = publisherService;
        _albumService = albumService;
    }

    [RelayCommand]
    async Task OnProfileIconClicked()
    {
        bool answer = await Shell.Current.DisplayAlert("Logout", "Would you like to log out?", "Yes", "No");
        if (answer)
        {
            Application.Current!.MainPage = new AppShell();
            Preferences.Default.Set("currentUserId", -1);
        }
    }

    public async Task GetActivePublisher()
    {
        Publisher = await _publisherService.GetCurrentUser();
    }

    [RelayCommand]
    async Task ViewProfile()
    {
        await GetActivePublisher();
        await Shell.Current.Navigation.PushAsync(new PubViewProfile(this));
    }

    public async void GetAlbums()
    {
        Albums = await _albumService.GetAllAsync(); //Should only get albums by the current publisher, temp implementation
    }

    [RelayCommand]  
    async Task Return()
    {
        // Navigate back
        await Shell.Current.GoToAsync("..");
    }

}



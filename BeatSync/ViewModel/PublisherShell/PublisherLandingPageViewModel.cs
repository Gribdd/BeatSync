namespace BeatSync.ViewModel.PublisherShell;

public partial class PublisherLandingPageViewModel : ObservableObject
{
    private readonly PublisherService publisherService;

    private Publisher _publisher = new();
    public Publisher Publisher
    {
        get { return _publisher; }
        set { SetProperty(ref _publisher, value); }
    }

    public PublisherLandingPageViewModel(PublisherService publisherService)
    {
        this.publisherService = publisherService;
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
        Publisher = await publisherService.GetCurrentUser();
    }

    [RelayCommand]
    async Task ViewProfile()
    {
        await GetActivePublisher();
        //await Shell.Current.Navigation.PushAsync(new ViewProfile(this));
    }

    [RelayCommand]  
    async Task Return()
    {
        // Navigate back
        await Shell.Current.GoToAsync("..");
    }

}



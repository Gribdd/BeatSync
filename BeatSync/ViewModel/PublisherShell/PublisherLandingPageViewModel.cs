
namespace BeatSync.ViewModel.PublisherShell;

public partial class PublisherLandingPageViewModel : ObservableObject
{
    private readonly PublisherService publisherService;

    [ObservableProperty]
    private Publisher _publisher = new();

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
}

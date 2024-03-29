
namespace BeatSync.ViewModel.PublisherShell;

[QueryProperty(nameof(Publisher), nameof(Publisher))]
public partial class PubUserHistoryViewModel : ObservableObject
{
	private PublisherService _publisherService;

    [ObservableProperty]
    private ObservableCollection<History> _userHistories = new();

	[ObservableProperty]
	private ObservableCollection<History> _filteredHistories = new();

    [ObservableProperty]
    private Publisher _publisher = new();

    public PubUserHistoryViewModel(PublisherService publisherService)
	{
		_publisherService = publisherService;
	}


	[RelayCommand]
	void Logout()
	{
        Shell.Current.FlyoutIsPresented = !Shell.Current.FlyoutIsPresented;
    }

    [RelayCommand]
    public async Task GetUserHistories()
    {
        var currentUser = await _publisherService.GetCurrentUser();
        UserHistories = await _publisherService.LoadUserHistoriesAsync();
        FilteredHistories = new ObservableCollection<History>(UserHistories.Where(history => history.UserId == currentUser.Id));
    }


}


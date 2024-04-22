namespace BeatSync.ViewModel.PublisherShell;

public partial class PubRecentlyPlayedViewModel : ObservableObject
{
	private readonly PublisherService _publisherService;
    private const int MaxRecentlyPlayedSongs = 10;

	[ObservableProperty]
	private Publisher _publisher = new();
	[ObservableProperty]	
	private ObservableCollection<Song> _recentlyPlayedSongs = new();

    public PubRecentlyPlayedViewModel(PublisherService publisherService)
	{
		_publisherService = publisherService;
		RecentlyPlayedSongs = new ObservableCollection<Song>();	
	}

    public void AddRecentlyPlayedSong(Song song)
    {
        // If the collection already contains the song, remove it first
        if (RecentlyPlayedSongs.Contains(song))
        {
            RecentlyPlayedSongs.Remove(song);
        }

        // Add the new song to the collection
        RecentlyPlayedSongs.Add(song);

        // If the collection exceeds the maximum limit, remove the oldest song
        while (RecentlyPlayedSongs.Count > MaxRecentlyPlayedSongs)
        {
            RecentlyPlayedSongs.RemoveAt(0);
        }
    }



    [RelayCommand]
    async Task Return()
    {
        await Shell.Current.GoToAsync("..");
    }
}
namespace BeatSync.ViewModel.Users;

public partial class CustomerRecentlyPlayedViewModel : ObservableObject
{
	private readonly UserService _userService;
    private const int MaxRecentlyPlayedSongs = 10;

    [ObservableProperty]
	private User _user = new();
	[ObservableProperty]
	private ObservableCollection<Song> _recentlyPlayedSongs = new();
	
	public CustomerRecentlyPlayedViewModel(UserService userService)
	{
		_userService = userService;
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
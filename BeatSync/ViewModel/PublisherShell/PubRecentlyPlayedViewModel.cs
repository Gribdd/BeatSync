namespace BeatSync.ViewModel.PublisherShell;

public partial class PubRecentlyPlayedViewModel : ObservableObject
{
    private readonly PublisherService _publisherService;
    private readonly SongService _songService;
    private readonly HistoryService historyService;
    private const int MAX_RECENTLY_PLAYED_SONGS = 10;


    [ObservableProperty]
    private Publisher _publisher = new();

    [ObservableProperty]
    private ObservableCollection<Song> _recentlyPlayedSongs = new();

    public PubRecentlyPlayedViewModel(PublisherService publisherService, SongService songService, HistoryService historyService)
    {
        _publisherService = publisherService;
        _songService = songService;
        this.historyService = historyService;
        RecentlyPlayedSongs = new ObservableCollection<Song>();
    }

    public async void AddRecentlyPlayedSong(Song song)
    {
        //LoadRecentlyPlayed();

        //// If the collection already contains the song, remove it first
        //if (RecentlyPlayedSongs.Contains(song))
        //{
        //    RecentlyPlayedSongs.Remove(song);
        //}

        ////// Add the new song to the collection
        //RecentlyPlayedSongs.Add(song);

        ////// If the collection exceeds the maximum limit, remove the oldest song
        //while (RecentlyPlayedSongs.Count > MAX_RECENTLY_PLAYED_SONGS)
        //{
        //    RecentlyPlayedSongs.RemoveAt(0);
        //}

        var publisher = await _publisherService.GetCurrentUser();

        var history = new History
        {
            AccountType = publisher.AccountType,
            UserId = publisher.Id, 
            SongId = song.Id,
            SongName = song.Name
        };

        await historyService.AddAsync(history);
    }

    public async void LoadRecentlyPlayed()
    {
        var publisher = await _publisherService.GetCurrentUser();
        var histories = await historyService.GetActiveAsync();
        var filtedHistories = new ObservableCollection<History>(histories.Where(h => h.UserId == publisher.Id));
        List<int> songIds = filtedHistories.Select(h => h.SongId).Distinct().ToList();
        var songs = await _songService.GetSongsBySongIds(songIds);

        RecentlyPlayedSongs = songs;
        
    }



    [RelayCommand]
    async Task Return()
    {
        await Shell.Current.GoToAsync("..");
    }
}
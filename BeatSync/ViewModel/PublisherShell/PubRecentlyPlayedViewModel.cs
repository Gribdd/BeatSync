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

        var publisher = await _publisherService.GetCurrentUser();

        var history = new History
        {
            AccountType = publisher.AccountType,
            UserId = publisher.Id, 
            SongId = song.Id,
            SongName = song.Name
        };

        var histories = await historyService.GetAllAsync();
        foreach(var historiesItem in histories)
        {
            if(historiesItem.AccountType == history.AccountType && historiesItem.UserId == history.UserId && historiesItem.SongId == history.SongId)
            {
                await historyService.DeleteAsync(historiesItem.Id);
            }
        }

        await historyService.AddAsync(history);
    }

    public async void LoadRecentlyPlayed()
    {
        var publisher = await _publisherService.GetCurrentUser();
        var histories = await historyService.GetActiveAsync();
        var filteredHistories = histories.Where(h => h.UserId == publisher.Id).OrderBy(h => h.TimeStamp);
        List<int> songIds = filteredHistories.Select(h => h.SongId).Distinct().ToList();
        var songs = await _songService.GetSongsBySongIds(songIds);
        var sortedSongs = new ObservableCollection<BeatSync.Models.Song>(songs.Where(s => songIds.Contains(s.Id)).OrderBy(s => filteredHistories.First(h => h.SongId == s.Id).TimeStamp).Reverse());
        RecentlyPlayedSongs = sortedSongs;
    }



    [RelayCommand]
    async Task Return()
    {
        await Shell.Current.GoToAsync("..");
    }
}
namespace BeatSync.ViewModel.Users;

[QueryProperty(nameof(Playlist), nameof(Playlist))]
[QueryProperty(nameof(User), nameof(User))]
public partial class AddPlaylistSongsCustomerViewModel : ObservableObject
{
    private readonly PlaylistService _playlistService;
    private readonly PlaylistSongService _playlistSongService;
    private readonly SongService _songService;
    private readonly UserService _userService;
    private readonly LikesService _likesService;
    [ObservableProperty]
    private Playlist _playlist = new();

    [ObservableProperty]
    private User _user = new();

    [ObservableProperty]
    private ObservableCollection<Song> _songs = new();

    [ObservableProperty]
    private ObservableCollection<PlaylistSongs> _playlistSongs = new();

    public AddPlaylistSongsCustomerViewModel(
        PlaylistService playlistService,
        PlaylistSongService playlistSongService,
        SongService songService,
        UserService userService,
        LikesService likesService)
    {
        _playlistService = playlistService;
        _playlistSongService = playlistSongService;
        _songService = songService;
        _userService = userService;
        _likesService = likesService;
    }

    [RelayCommand]
    async Task Logout()
    {
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    async Task NavigateToAddPlaylistSongsSearch()
    {
        var navigationParameter = new Dictionary<string, object>
        {
            {"PlaylistId", Playlist.Id },
        };

        await Shell.Current.GoToAsync($"{nameof(AddPlaylistSongsSearch)}", navigationParameter);
    }

    [RelayCommand]
    async Task LikeSong(Song song)
    {
        if (song.IsLike)
        {
            song.IsLike = !song.IsLike;
            var likes = await _likesService.GetLikesByUserIdAsync(User.Id);
            var like = likes.FirstOrDefault(like => like.SongID == song.Id && like.UserID == User.Id);
            bool isRemove = likes.Remove(like!);

            if (isRemove)
            {
                await _likesService.UpdateAsync(like);
            }
            return;
        }

            await AddLike(song);
    }

    private async Task AddLike(Song song)
    {
        var like = new Likes
        {
            SongID = song.Id,
            UserID = User.Id,
            CreatedAt = DateTime.Now
        };

        await _likesService.AddAsync(like);
        song.IsLike = true;
    }

    [RelayCommand]
    async Task RemoveSongFromPlaylist(Song song)
    {
        bool canDelete = await Shell.Current.DisplayAlert("Remove Song", "Are you sure you want to remove this song from the playlist?", "Yes", "No");
        var playlistSong = PlaylistSongs.FirstOrDefault(playlistSong => playlistSong.SongId == song.Id);
        if (playlistSong != null && canDelete)
        {
            await _playlistSongService.DeleteAsync(playlistSong.Id);
            if (Playlist.SongCount > 0)
                Playlist.SongCount--;
            await Task.Delay(1000);
            await _playlistService.UpdateAsync(Playlist);
            PlaylistSongs.Remove(playlistSong);
            Songs.Remove(song);
        }
    }


    public async Task GetSongsByPlaylistId()
    {
        var songIds = PlaylistSongs.Select(playlistSong => playlistSong.SongId).ToList();
        Songs = await _songService.GetSongsBySongIds(songIds);
    }

    public async Task GetPlaylistSongsPlaylistId()
    {
        PlaylistSongs = await _playlistSongService.GetPlaylistSongsByPlaylistIdAsync(Playlist.Id);
    }

    public async Task GetUserLikeSongs()
    {
        var likes = await _likesService.GetLikesByUserIdAsync(User.Id);
        //make the song islike true if the user liked the song
        foreach (var song in Songs)
        {
            if (likes.Any(like => like.SongID == song.Id))
                song.IsLike = true;
        }
    }
}

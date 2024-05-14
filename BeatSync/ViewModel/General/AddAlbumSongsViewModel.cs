
using BeatSync.Services.Service;

namespace BeatSync.ViewModel.General;

//first parameter receives the data
//second parameter is the parameter id specied in the shell route
[QueryProperty(nameof(Album), nameof(Album))]
[QueryProperty(nameof(Account), nameof(Account))]
public partial class AddAlbumSongsViewModel : ObservableObject
{
    private AlbumService albumService;
    private SongService songService;

    [ObservableProperty]
    private Album _album = new();

    [ObservableProperty]
    private ObservableCollection<object> _account = new();

    [ObservableProperty]
    private ObservableCollection<Song> _songs = new();


    public AddAlbumSongsViewModel(AlbumService albumService, SongService songService)
    {
        this.albumService = albumService;
        this.songService = songService;
    }

    public async void GetSongsByArtistId()
    {
        Songs = await songService.GetSongsByArtistIdAsync(Album.ArtistId);
    }

    [RelayCommand]
    async Task Return()
    {
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    async Task AddSongToAlbum()
    {
        string[] songNames = Songs.Select(s => s.Name!).ToArray();
        string selectedSongName = await Shell.Current.DisplayActionSheet("Add song to album", "Cancel", null, songNames);

        if(selectedSongName == null)
        {
            return;
        }

        var selectedSong = await songService.GetByNameAsync(selectedSongName);
        if(selectedSong == null)
        {
            return;
        }

        if (selectedSong!.AlbumId != null)
        {
            await Shell.Current.DisplayAlert("Add Album Song Error", "Song is already in an album", "OK");
            return;
        }

        //Initialize the album's songs collection if it's null
        Album.Songs ??= new();

        Album.Songs!.Add(selectedSong!);

        selectedSong.AlbumId = Album.Id;
        await albumService.UpdateAlbumSongs(Album);
        await songService.UpdateAsync(selectedSong);
    }

    [RelayCommand]
    async Task RemoveSongFromAlbum(Song song)
    {
        Album.Songs!.Remove(song);
        song.AlbumId = null;
        await albumService.UpdateAlbumSongs(Album);
        await songService.UpdateAsync(song);
    }
}

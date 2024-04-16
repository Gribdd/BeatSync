
using BeatSync.Services.Service;

namespace BeatSync.ViewModel.Admin;

public partial class AddSongViewModel : ObservableObject
{
    private const string Directory = "Songs";
    private readonly ArtistService _artistService;
    private readonly SongService _songService;
    private readonly FileUploadService _fileUploadService;
    private FileResult? fileResultSongImage;
    private FileResult? fileResultSong;

    [ObservableProperty]
    private bool _isVisible = false;

    [ObservableProperty]
    private Song _song = new();

    [ObservableProperty]
    private Artist _selectedArtist = new();

    [ObservableProperty]
    private ObservableCollection<Artist> _artists = new();

    public string[] Genres { get; set; } = { "Rap", "Pop", "Indie", "OPM", "Punk Rock" };

    public AddSongViewModel( 
        ArtistService artistService, 
        SongService songService, 
        FileUploadService fileUploadService)
    {
        _artistService = artistService;
        _songService = songService;
        _fileUploadService = fileUploadService;
    }

    [RelayCommand]
    async Task AddSong()
    {
        Song.ArtistID = SelectedArtist.Id;
        Song.ArtistName = $"{SelectedArtist.FirstName} {SelectedArtist.LastName}";

        var (isValid, message) = Song.IsValid();
        if (!isValid)
        {
            await Shell.Current.DisplayAlert("Error!", message, "Ok");
            return;
        }

        await _songService.AddAsync(Song);
        
        File.Copy(fileResultSongImage!.FullPath, Song.ImageFilePath!);
        File.Copy(fileResultSong!.FullPath, Song.FilePath!);
        await Shell.Current.DisplayAlert("Add Song", "Song successfully added", "OK");
        await Shell.Current.GoToAsync("..");
        
    }

    [RelayCommand]
    async Task UploadImage()
    {
        (fileResultSongImage, Song.ImageFilePath) = await _fileUploadService.UploadImage(Song.Name, Directory);
    }

    [RelayCommand]
    async Task UploadSong()
    {
        if (string.IsNullOrEmpty(Song.Name))
        {
            await Shell.Current.DisplayAlert("Upload song", "Please enter song name first", "OK");
            return;
        }

        (fileResultSong, Song.FilePath) = await _fileUploadService.UploadSong(Song.Name);
    }

    [RelayCommand]
    async Task Return()
    {
        await Shell.Current.GoToAsync("..");
    }

    public async Task PopulateArtist()
    {
        Artists = await _artistService.GetAllAsync();
    }
}

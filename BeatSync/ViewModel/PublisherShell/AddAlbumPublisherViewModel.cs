using BeatSync.Services.Service;

namespace BeatSync.ViewModel.PublisherShell;

public partial class AddAlbumPublisherViewModel : ObservableObject
{
    //directory for storing images
    private const string Directory = "Albums";
    private readonly AlbumService _albumService;
    private readonly ArtistService _artistService;
    private readonly FileUploadService _fileUploadService;
    private FileResult? fileResultAlbumCoverImage;

    [ObservableProperty]
    private Album _album = new();

    [ObservableProperty]
    private ObservableCollection<Artist> _artists = new();

    [ObservableProperty]
    private Artist _selectedArtist = new();

    public AddAlbumPublisherViewModel(
        AlbumService albumService, 
        ArtistService artistService, 
        FileUploadService fileUploadService)
    {
        _albumService = albumService;
        _artistService = artistService;
        _fileUploadService = fileUploadService;
    }

    [RelayCommand]
    async Task Return()
    {
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    async Task UploadImage()
    {
        (fileResultAlbumCoverImage, Album.ImageFilePath) = await _fileUploadService.UploadImage(Album.Name, Directory);
    }

    [RelayCommand]
    async Task AddAlbum()
    {
        Album.ArtistId = SelectedArtist.Id;
        Album.ArtistName = SelectedArtist.FullName;

        var (isValid, message) = Album.IsValid();
        if (!isValid)
        {
            await Shell.Current.DisplayAlert("Error!", message, "Ok");
            return;
        }

        if (!await IsAlbumUnique(Album.Name!, Album.ArtistId))
        {
            await Shell.Current.DisplayAlert("Error!", "Album name already exists", "Ok");
            return;
        }

        await _albumService.AddAsync(Album);
        File.Copy(fileResultAlbumCoverImage!.FullPath, Album.ImageFilePath!);
        await Shell.Current.DisplayAlert("Add Album", "Album successfully added", "OK");
        await Shell.Current.GoToAsync("..");
    }
    public async void GetArtists()
    {
        Artists = await _artistService.GetActiveAsync();
    }

    //validation to check if the album name is unique
    //checks by getting album by name and if it is null then the album name is unique
    private async Task<bool> IsAlbumUnique(string albumName, int artistId)
    {
        var album = await _albumService.GetByNameAsync(albumName);
        //name can be duplicate if the artist is different
        return album == null || album.ArtistId != artistId;
    }
}

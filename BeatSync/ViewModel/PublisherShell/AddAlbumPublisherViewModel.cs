using BeatSync.Models;
using BeatSync.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;


namespace BeatSync.ViewModel.PublisherShell;

public partial class AddAlbumPublisherViewModel : ObservableObject
{
    private const string Directory = "Albums";
    private AlbumService albumService;
    private ArtistService artistService;
    private FileUploadService fileUploadService;
    private FileResult? fileResultAlbumCoverImage;

    [ObservableProperty]
    private Album _album = new();

    [ObservableProperty]
    private ObservableCollection<Artist> _artists = new();

    [ObservableProperty]
    private Artist _selectedArtist = new();

    public AddAlbumPublisherViewModel(AlbumService albumService, ArtistService artistService, FileUploadService fileUploadService)
    {
        this.albumService = albumService;
        this.artistService = artistService;
        this.fileUploadService = fileUploadService;
    }

    [RelayCommand]
    async Task Return()
    {
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    async Task UploadImage()
    {
        if (string.IsNullOrEmpty(Album.Name))
        {
            await Shell.Current.DisplayAlert("Upload picture", "Please enter an album name first", "OK");
            return;
        }

        (fileResultAlbumCoverImage, Album.ImageFilePath) = await fileUploadService.UploadImage(Album.Name, Directory);
    }

    [RelayCommand]
    async Task AddAlbum()
    {
        ValidateFields();

        Album.ArtistId = SelectedArtist.Id;
        Album.ArtistName = SelectedArtist.FullName;

        if (await albumService.AddAlbumAsync(Album))
        {
            File.Copy(fileResultAlbumCoverImage!.FullPath, Album.ImageFilePath!);
            await Shell.Current.DisplayAlert("Add Album", "Album successfully added", "OK");
            await Shell.Current.GoToAsync("..");
        }
        else
        {
            await Shell.Current.DisplayAlert("Add Album", "Please enter all fields", "OK");
        }
    }
    
    private async void ValidateFields()
    {
        if (SelectedArtist == null)
        {
            await Shell.Current.DisplayAlert("Oops.", "Please select an artist first.", "OK");
            return;
        }

        if (string.IsNullOrEmpty(Album.Name))
        {
            await Shell.Current.DisplayAlert("Oops.", "Please enter a name for the album.", "OK");

        }
    }

    public async void GetArtists()
    {
        Artists = await artistService.GetActiveArtistAsync();
    }
}

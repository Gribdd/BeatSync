using BeatSync.Models;
using BeatSync.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace BeatSync.ViewModel.Admin;

public partial class AddSongViewModel : ObservableObject
{
    private const string Directory = "Songs";
    private AdminService adminService;
    private ArtistService artistService;
    private SongService songService;
    private FileUploadService fileUploadService;
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

    public AddSongViewModel(AdminService adminService, ArtistService artistService, SongService songService, FileUploadService fileUploadService)
    {
        this.adminService = adminService;
        this.artistService = artistService;
        this.songService = songService;
        this.fileUploadService = fileUploadService;
    }

    [RelayCommand]
    async Task AddSong()
    {
        if(SelectedArtist == null)
        {
            await Shell.Current.DisplayAlert("Add Song", "Please select an artist first", "OK");
            return;
        }

        if (string.IsNullOrEmpty(Song.ImageFilePath))
        {
            await Shell.Current.DisplayAlert("Upload picture", "Please upload song picture first", "OK");
            return;
        }

        Song.ArtistID = SelectedArtist.Id;
        Song.ArtistName = $"{SelectedArtist.FirstName} {SelectedArtist.LastName}";
        if (await songService.AddSongAsync(Song))
        {
            File.Copy(fileResultSongImage!.FullPath, Song.ImageFilePath);
            File.Copy(fileResultSong!.FullPath, Song.FilePath!);
            await Shell.Current.DisplayAlert("Add Song", "Song successfully added", "OK");
            await Shell.Current.GoToAsync("..");
        }
        else
        {
            await Shell.Current.DisplayAlert("Add Song", "Please enter all fields", "OK");
        }
    }

    [RelayCommand]
    async Task UploadImage()
    {
        if (string.IsNullOrEmpty(Song.Name))
        {
            await Shell.Current.DisplayAlert("Upload picture", "Please enter song name first", "OK");
            return;
        }

        (fileResultSongImage, Song.ImageFilePath) = await fileUploadService.UploadImage(Song.Name, Directory);
    }

    [RelayCommand]
    async Task UploadSong()
    {
        if (string.IsNullOrEmpty(Song.Name))
        {
            await Shell.Current.DisplayAlert("Upload song", "Please enter song name first", "OK");
            return;
        }

        (fileResultSong, Song.FilePath) = await fileUploadService.UploadSong(Song.Name);
    }

    [RelayCommand]
    async Task Return()
    {
        await Shell.Current.GoToAsync("..");
    }

    public async Task PopulateArtist()
    {
        Artists = await artistService.GetActiveArtistAsync();
    }
}

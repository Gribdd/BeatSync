using BeatSync.Models;
using BeatSync.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace BeatSync.ViewModel.Admin;

public partial class AddSongViewModel : ObservableObject
{
    private AdminService _adminService;
    private FileResult? _fileResult;

    [ObservableProperty]
    private Song _song = new();

    [ObservableProperty]
    private Artist _selectedArtist = new();

    [ObservableProperty]
    private ObservableCollection<Artist> _artists = new();

    public string[] Genres { get; set; } = { "Rap", "Pop", "Indie", "OPM", "Punk Rock" };
    public AddSongViewModel(AdminService adminService)
    {
        _adminService = adminService;
    }

    [RelayCommand]
    async Task AddSong()
    {
        Song.ArtistID = SelectedArtist.Id;
        Song.ArtistName = $"{SelectedArtist.FirstName} {SelectedArtist.LastName}";

        if (string.IsNullOrEmpty(Song.ImageFilePath))
        {
            await Shell.Current.DisplayAlert("Upload picture", "Please upload song picture first", "OK");
            return;
        }

        if (await _adminService.AddSongAsync(Song))
        {
            File.Copy(_fileResult!.FullPath, Song.ImageFilePath);

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

        _fileResult = await FilePicker.PickAsync(new PickOptions
        {
            PickerTitle = "Please pick an image for the song",
            FileTypes = FilePickerFileType.Images
        });

        if (_fileResult == null)
        {
            return;
        }

        //make dir 
        string dir = Path.Combine(FileSystem.Current.AppDataDirectory, "Songs");
        _adminService.CreateDirectoryIfMissing(dir);

        Song.ImageFilePath = Path.Combine(dir, $"{Song.Name}.jpg");
        await Shell.Current.DisplayAlert("Upload picture", "Picture successfully uploaded ", "OK");

    }

    


    [RelayCommand]
    async Task Return()
    {
        await Shell.Current.GoToAsync("..");
    }

    public async Task PopulateArtist()
    {
        Artists = await _adminService.GetActiveArtistAsync();
    }
}

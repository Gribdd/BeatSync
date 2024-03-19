using BeatSync.Models;
using BeatSync.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatSync.ViewModel.PublisherShell;

public partial class AddAlbumPublisherViewModel : ObservableObject
{
    private AdminService _adminService;
    private FileResult? _fileResultAlbumCoverImage;

    [ObservableProperty]
    private Album _album = new();

    [ObservableProperty]
    private ObservableCollection<Artist> _artists = new();

    [ObservableProperty]
    private Artist _selectedArtist = new();

    public AddAlbumPublisherViewModel(AdminService adminService)
    {
        _adminService = adminService;
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

        _fileResultAlbumCoverImage = await FilePicker.PickAsync(new PickOptions
        {
            PickerTitle = "Please pick an image for the song",
            FileTypes = FilePickerFileType.Images
        });

        if (_fileResultAlbumCoverImage == null)
        {
            return;
        }

        //make dir 
        string dir = Path.Combine(FileSystem.Current.AppDataDirectory, "Albums");
        _adminService.CreateDirectoryIfMissing(dir);

        Album.ImageFilePath = Path.Combine(dir, $"{Album.Name}.jpg");
        await Shell.Current.DisplayAlert("Upload picture", "Picture successfully uploaded ", "OK");
    }

    [RelayCommand]
    async Task AddAlbum()
    {
        if(SelectedArtist == null)
        {
            await Shell.Current.DisplayAlert("Oops.", "Please select an artist first.", "OK");
            return;
        }

        if (string.IsNullOrEmpty(Album.Name))
        {
            await Shell.Current.DisplayAlert("Oops.", "Please enter a name for the album.", "OK");

        }

        Album.ArtistId = SelectedArtist.Id;
        if (await _adminService.AddAlbumAsync(Album))
        {
            await Shell.Current.DisplayAlert("Add Album", "Album successfully added", "OK");
            await Shell.Current.GoToAsync("..");
        }
        else
        {
            await Shell.Current.DisplayAlert("Add Album", "Please enter all fields", "OK");
        }
    }
    
    public async void GetArtists()
    {
        Artists = await _adminService.GetActiveArtistAsync();
    }
}

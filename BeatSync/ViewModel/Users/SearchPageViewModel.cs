using BeatSync.Models;
using BeatSync.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Linq;

using System.Threading.Tasks;

namespace BeatSync.ViewModel.Users;

public partial class SearchPageViewModel : ObservableObject
{

    [ObservableProperty]
    private string? _searchQuery;

    [ObservableProperty]
    private MyCollection _myCollection = new();


    [ObservableProperty]
    private ObservableCollection<object> _myList = new();

    private AdminService _adminService;
    private ArtistService _artistService;
    private AlbumService _albumService;
    private PublisherService _publisherService;
    private SongService _songService;
    private UserService _userService;

    public SearchPageViewModel(AdminService adminService, ArtistService artistService, AlbumService albumService, PublisherService publisherService, SongService songService, UserService userService)
    {
        _adminService = adminService;
        _artistService = artistService;
        _albumService = albumService;
        _publisherService = publisherService;
        _songService = songService;
        _userService = userService;
    }

    [RelayCommand]
    async Task Logout()
    {
        await _adminService.Logout();
    }

    [RelayCommand]
    public async Task SearchCommand()
    {
        _myCollection.Filter(SearchQuery);
        MyList.Clear(); 
        IsResultsVisible = true;

        bool found = false; 
        foreach (var item in _myCollection.FilteredCollection)
        {
            switch (item)
            {
                case BeatSync.Models.Song song when song.Name.Contains(SearchQuery):
                    MyList.Add(String.Concat(song.Name, " - Song"));
                    
                    found = true;
                    break;
                case BeatSync.Models.Album album when album.Name.Contains(SearchQuery):
                    MyList.Add(String.Concat(album.Name, " - Album"));
                    found = true;
                    break;
                case BeatSync.Models.Artist artist when artist.FullName.Contains(SearchQuery) || artist.FirstName.Contains(SearchQuery) || artist.LastName.Contains(SearchQuery):
                    MyList.Add(artist.FullName + " - Artist");
                    found = true;
                    break;
                case BeatSync.Models.Artist artist when artist.Username.Contains(SearchQuery):
                    MyList.Add(String.Concat(artist.Username, " - Artist"));
                    found = true;
                    break;
                case BeatSync.Models.Publisher publisher when publisher.FullName.Contains(SearchQuery) || publisher.FirstName.Contains(SearchQuery) || publisher.LastName.Contains(SearchQuery):
                    MyList.Add(String.Concat(publisher.FullName, " - Publisher"));
                    found = true;
                    break;
                case BeatSync.Models.Publisher publisher when publisher.Username.Contains(SearchQuery):
                    MyList.Add(String.Concat(publisher.Username, " - Publisher"));
                    found = true;
                    break;
                case BeatSync.Models.User user when user.FullName.Contains(SearchQuery) || user.FirstName.Contains(SearchQuery) || user.LastName.Contains(SearchQuery):
                    MyList.Add(String.Concat(user.FullName, " - User"));
                    found = true;
                    break;
                case BeatSync.Models.User user when user.Username.Contains(SearchQuery):
                    MyList.Add(String.Concat(user.Username, " - User"));
                    found = true;
                    break;
            }
        }

        // If no items were found, add default message
        if (!found)
        {
            string foundNothing = "No search result found.";
            MyList.Add(foundNothing);
        }
    }

    [ObservableProperty]
    private bool _isResultsVisible;

}
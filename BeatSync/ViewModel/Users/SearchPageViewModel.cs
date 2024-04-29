using System.Runtime.CompilerServices;
using BeatSync.Services.Service;

namespace BeatSync.ViewModel.Users;

public partial class SearchPageViewModel : ObservableObject
{
    private UserService _userService;

    private string? _searchQuery;
    public string? SearchQuery
    {
        get => _searchQuery;
        set
        {
            SetProperty(ref _searchQuery, value);
            Search();
        }
    }

    [ObservableProperty]
    private MyCollection _myCollection;

    [ObservableProperty]
    private User _user = new();

    [ObservableProperty]
    private bool _isResultsVisible;

    [ObservableProperty]
    private ObservableCollection<object> _myList = new();


    public SearchPageViewModel(UserService userService, MyCollection myCollection)
    {
        _userService = userService;
        _myCollection = myCollection;

    }


    [RelayCommand]
    void Logout()
    {
        Shell.Current.FlyoutIsPresented = !Shell.Current.FlyoutIsPresented;
    }

    [RelayCommand]
    void Search()
    {
        MyList?.Clear();
        MyCollection?.FilteredCollection?.Clear();
        MyCollection?.Filter(SearchQuery!);
        IsResultsVisible = true;
        //added playlists
        foreach (var item in MyCollection?.FilteredCollection ?? Enumerable.Empty<object>())
        {
            switch (item)
            {
                case BeatSync.Models.Song song when song.Name != null && song.Name.Contains(SearchQuery!):
                    MyList?.Add(song);
                    break;
                case BeatSync.Models.Album album when album.Name != null && album.Name.Contains(SearchQuery!):
                    MyList?.Add(album);
                    break;
                case BeatSync.Models.Artist artist when artist.FullName != null && (
                    artist.FullName.Contains(SearchQuery!) ||
                    artist.Username != null && artist.Username.Contains(SearchQuery!) ||
                    artist.FirstName != null && artist.FirstName.Contains(SearchQuery!) ||
                    artist.LastName != null && artist.LastName.Contains(SearchQuery!)):
                    MyList?.Add(artist);
                    break;
                case BeatSync.Models.Playlist playlist when playlist.Name != null && playlist.Name.Contains(SearchQuery!):
                    MyList?.Add(playlist);
                    break;
                    
            }
        }
    }


    public async void LoadCurrentUser()
    {
        User = await _userService.GetCurrentUser();
    }
}
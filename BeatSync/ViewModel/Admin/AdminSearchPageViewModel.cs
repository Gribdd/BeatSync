namespace BeatSync.ViewModel.Admin;

public partial class AdminSearchPageViewModel: ObservableObject
{

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


    public AdminSearchPageViewModel( MyCollection myCollection)
    {
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
                case BeatSync.Models.Publisher publisher when publisher.FullName != null && (
                    publisher.FullName.Contains(SearchQuery!) ||
                    publisher.Username != null && publisher.Username.Contains(SearchQuery!) ||
                    publisher.FirstName != null && publisher.FirstName.Contains(SearchQuery!) ||
                    publisher.LastName != null && publisher.LastName.Contains(SearchQuery!)):
                    MyList?.Add(publisher);
                    break;
                case BeatSync.Models.User user when user.FullName != null && (
                    user.FullName.Contains(SearchQuery!) ||
                    user.Username != null && user.Username.Contains(SearchQuery!) ||
                    user.FirstName != null && user.FirstName.Contains(SearchQuery!) ||
                    user.LastName != null && user.LastName.Contains(SearchQuery!)):
                    MyList?.Add(user);
                    break;

            }
        }
    }

    [RelayCommand]
    async Task Return()
    {
        await Shell.Current.GoToAsync("..");
    }
}
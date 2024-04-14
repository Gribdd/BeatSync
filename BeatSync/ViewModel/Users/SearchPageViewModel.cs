using System.Runtime.CompilerServices;

namespace BeatSync.ViewModel.Users;

public partial class SearchPageViewModel : ObservableObject
{
    private UserService _userService;

    [ObservableProperty]
    private string? _searchQuery;

    [ObservableProperty]
    private MyCollection _myCollection;

    [ObservableProperty]
    private User _user = new();

    [ObservableProperty]
    private bool _isResultsVisible;

    [ObservableProperty]
    private ObservableCollection<object> _myList = new();


    public SearchPageViewModel(UserService userService)
    {
        _userService = userService;

    }


    [RelayCommand]
    void Logout()
    {
        Shell.Current.FlyoutIsPresented = !Shell.Current.FlyoutIsPresented;
    }

    [RelayCommand]
    public Task SearchCommand()
    {
        MyCollection.Filter(SearchQuery!);
        MyList.Clear(); 
        IsResultsVisible = true;

        bool found = false; 
        foreach (var item in MyCollection.FilteredCollection)
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

        return Task.CompletedTask;
    }

    public async void LoadCurrentUser()
    {
        User = await _userService.GetCurrentUser();
    }
}
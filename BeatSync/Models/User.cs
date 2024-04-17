namespace BeatSync.Models;

public partial class User : Account
{
    [ObservableProperty]
    private List<int> _favoriteSongsId = new();

    public User()
    {
        AccountType = 3;
    }
}

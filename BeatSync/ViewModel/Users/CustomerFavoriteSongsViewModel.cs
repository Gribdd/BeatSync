namespace BeatSync.ViewModel.Users;

[QueryProperty(nameof(FavoriteSongs), nameof(FavoriteSongs))]
public partial class CustomerFavoriteSongsViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Song> _favoriteSongs = new();

    [RelayCommand]
    async Task Return()
    {
        await Shell.Current.GoToAsync("..");
    }
}

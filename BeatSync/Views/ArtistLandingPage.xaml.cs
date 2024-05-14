using BeatSync.ViewModel.ArtistShell;

namespace BeatSync.Views;

public partial class ArtistLandingPage : Shell
{
    ArtistLandingPageViewModel _vm;
    public ArtistLandingPage(ArtistLandingPageViewModel vm)
    {
        Routing.RegisterRoute($"songs/{nameof(AddSong)}", typeof(AddSong));
        Routing.RegisterRoute($"library/{nameof(AddAlbumPublisher)}", typeof(AddAlbumPublisher)); //add/revise for artist side
        Routing.RegisterRoute($"library/{nameof(AddAlbumSongs)}", typeof(AddAlbumSongs));
        Routing.RegisterRoute($"library/{nameof(PubRecentlyPlayed)}",typeof(PubRecentlyPlayed)); //add/revise for artist side
        Routing.RegisterRoute($"library/{nameof(CustomerFavoriteSongs)}", typeof(CustomerFavoriteSongs)); //add/revise for artist side
        Routing.RegisterRoute($"library/{nameof(AlbumSearchPage)}", typeof(AlbumSearchPage)); //add/revise for artist side
        Routing.RegisterRoute(nameof(ArtistViewProfile), typeof(ArtistViewProfile));

        BindingContext = _vm = vm;
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        var result = Task.Run(async () => await _vm.GetActiveArtist());
    }
}
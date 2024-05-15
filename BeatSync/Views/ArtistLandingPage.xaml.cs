using BeatSync.ViewModel.ArtistShell;

namespace BeatSync.Views;

public partial class ArtistLandingPage : Shell
{
    ArtistLandingPageViewModel _vm;
    public ArtistLandingPage(ArtistLandingPageViewModel vm)
    {
        Routing.RegisterRoute($"songs/{nameof(AddSong)}", typeof(AddSong));
        Routing.RegisterRoute($"library/{nameof(AddAlbumPublisher)}", typeof(AddAlbumPublisher)); 
        Routing.RegisterRoute($"library/{nameof(AddAlbumSongs)}", typeof(AddAlbumSongs));
        Routing.RegisterRoute($"library/{nameof(PubRecentlyPlayed)}",typeof(PubRecentlyPlayed)); 
        Routing.RegisterRoute($"library/{nameof(CustomerFavoriteSongs)}", typeof(CustomerFavoriteSongs)); 
        Routing.RegisterRoute($"library/{nameof(AlbumSearchPage)}", typeof(AlbumSearchPage)); 
        Routing.RegisterRoute(nameof(ArtistViewProfile), typeof(ArtistViewProfile));
        Routing.RegisterRoute($"songs/{nameof(SongSearchPage)}", typeof(SongSearchPage));
        Routing.RegisterRoute(nameof(LandingPageSearch), typeof(LandingPageSearch));
        //Routing.RegisterRoute($"landingpage/{nameof(LandingPageSearch)}", typeof(LandingPageSearch));


        BindingContext = _vm = vm;
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        var result = Task.Run(async () => await _vm.GetActiveArtist());
    }
}
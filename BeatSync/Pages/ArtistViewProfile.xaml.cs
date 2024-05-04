using BeatSync.ViewModel.ArtistShell;
namespace BeatSync.Pages;

public partial class ArtistViewProfile : ContentPage
{
    ArtistLandingPageViewModel _vm;
    public ArtistViewProfile(ArtistLandingPageViewModel vm)
    {
        BindingContext = _vm = vm;
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _vm.GetActiveArtist();
        _vm.GetAlbums();
        _vm.GetSongs();
    }
}

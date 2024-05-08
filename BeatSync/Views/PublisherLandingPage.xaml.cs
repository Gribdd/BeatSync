namespace BeatSync.Views;

public partial class PublisherLandingPage : Shell
{
    PublisherLandingPageViewModel _vm;
    public PublisherLandingPage(PublisherLandingPageViewModel vm)
    {
        Routing.RegisterRoute($"songs/{nameof(AddSong)}", typeof(AddSong));
        Routing.RegisterRoute($"library/{nameof(AddAlbumPublisher)}", typeof(AddAlbumPublisher));
        Routing.RegisterRoute($"library/{nameof(AddAlbumSongs)}", typeof(AddAlbumSongs));
        Routing.RegisterRoute(nameof(ViewProfile), typeof(ViewProfile));
        Routing.RegisterRoute(nameof(PubRecentlyPlayed), typeof(PubRecentlyPlayed));


        BindingContext = _vm = vm;
        InitializeComponent();
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        var result = Task.Run(async () => await _vm.GetActivePublisher());
    }

}


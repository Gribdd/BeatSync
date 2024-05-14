namespace BeatSync.Views;

public partial class PublisherLandingPage : Shell
{
    PublisherLandingPageViewModel _vm;
    public PublisherLandingPage(PublisherLandingPageViewModel vm)
    {
        Routing.RegisterRoute($"songs/{nameof(AddSong)}", typeof(AddSong));
        Routing.RegisterRoute($"library/{nameof(AddAlbumPublisher)}", typeof(AddAlbumPublisher));
        Routing.RegisterRoute($"library/{nameof(AddAlbumSongs)}", typeof(AddAlbumSongs));
        Routing.RegisterRoute($"library/{nameof(AlbumSearchPage)}", typeof(AlbumSearchPage));
        Routing.RegisterRoute(nameof(ViewProfile), typeof(ViewProfile));
        Routing.RegisterRoute(nameof(PubRecentlyPlayed), typeof(PubRecentlyPlayed));


        BindingContext = _vm = vm;
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        var watch = System.Diagnostics.Stopwatch.StartNew();
        Task.Run(async () => await _vm.GetActivePublisher());

        watch.Stop();
        var elapsedMs = watch.ElapsedMilliseconds;
        System.Diagnostics.Debug.WriteLine($"PublisherLandingPage.OnAppearing took {elapsedMs}ms");
    }

}


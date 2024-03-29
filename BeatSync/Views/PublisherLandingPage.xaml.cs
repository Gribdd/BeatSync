
namespace BeatSync.Views;

public partial class PublisherLandingPage : Shell
{
    PublisherLandingPageViewModel _vm;
	public PublisherLandingPage(PublisherLandingPageViewModel vm)
	{
		Routing.RegisterRoute($"songs/{nameof(AddSong)}", typeof(AddSong));
		Routing.RegisterRoute($"library/{nameof(AddAlbumPublisher)}", typeof(AddAlbumPublisher));
		Routing.RegisterRoute($"library/{nameof(AddAlbumSongs)}", typeof(AddAlbumSongs));

		BindingContext = _vm = vm;
		InitializeComponent();
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await _vm.GetActivePublisher();
    }

    protected override void OnNavigating(ShellNavigatingEventArgs args)
    {
        var navigationParameter = new Dictionary<string, object>
        {
            {"Publisher", _vm.Publisher}
        };

        base.OnNavigating(args);
        if (args.Target.Location.OriginalString.Contains("library"))
        {
            Shell.Current.GoToAsync("library", navigationParameter);
        }
        else if (args.Target.Location.OriginalString.Contains("songs"))
        {
            Shell.Current.GoToAsync("songs", navigationParameter);
        }
        else if (args.Target.Location.OriginalString.Contains("history"))
        {
            Shell.Current.GoToAsync("history", navigationParameter);
        }
        else
        {
            Shell.Current.GoToAsync("home", navigationParameter);
        }
    }
}
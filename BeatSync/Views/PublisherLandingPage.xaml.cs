using BeatSync.Pages;

namespace BeatSync.Views;

public partial class PublisherLandingPage : Shell
{
	public PublisherLandingPage()
	{
		Routing.RegisterRoute(nameof(LandingPage), typeof(LandingPage));
		Routing.RegisterRoute(nameof(SongManagementPub), typeof(SongManagementPub));
		Routing.RegisterRoute(nameof(LibraryPage), typeof(LibraryPage));

		InitializeComponent();
	}
}
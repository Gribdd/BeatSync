using BeatSync.Pages;

namespace BeatSync.Views;

public partial class PublisherLandingPage : Shell
{
	public PublisherLandingPage()
	{
		Routing.RegisterRoute($"songs/{nameof(AddSong)}", typeof(AddSong));
		Routing.RegisterRoute($"library/{nameof(AddAlbumPublisher)}", typeof(AddAlbumPublisher));
		Routing.RegisterRoute($"library/{nameof(AddAlbumSongs)}", typeof(AddAlbumSongs));

		InitializeComponent();
	}
}
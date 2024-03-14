
using BeatSync.Pages;

namespace BeatSync.Views;

public partial class Admin_LandingPage : Shell
{
	public Admin_LandingPage()
	{

        //Routing.RegisterRoute(nameof(ArtistManagement), typeof(ArtistManagement));
        //Routing.RegisterRoute(nameof(UserManagement), typeof(UserManagement));
        //Routing.RegisterRoute(nameof(SongManagement), typeof(SongManagement));
        //Routing.RegisterRoute(nameof(PublisherManagement), typeof(PublisherManagement));
        //Routing.RegisterRoute(nameof(LandingPage), typeof(LandingPage));

        Routing.RegisterRoute(nameof(AddPublisher), typeof(AddPublisher));
        Routing.RegisterRoute(nameof(AddArtist), typeof(AddArtist));
        Routing.RegisterRoute(nameof(AddSong), typeof(AddSong));
        Routing.RegisterRoute(nameof(AddUser), typeof(AddUser));

        InitializeComponent();
	}
}
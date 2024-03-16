
using BeatSync.Pages;

namespace BeatSync.Views;

public partial class Admin_LandingPage : Shell
{
	public Admin_LandingPage()
	{

        Routing.RegisterRoute(nameof(AddPublisher), typeof(AddPublisher));
        Routing.RegisterRoute(nameof(AddArtist), typeof(AddArtist));
        Routing.RegisterRoute(nameof(AddSong), typeof(AddSong));
        Routing.RegisterRoute(nameof(AddUser), typeof(AddUser));

        InitializeComponent();
	}
}
using BeatSync.Pages;
using BeatSync.Views;

namespace BeatSync
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(Admin_LandingPage), typeof(Admin_LandingPage));
            Routing.RegisterRoute(nameof(Admin_LoginPage), typeof(Admin_LoginPage));
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));

            Routing.RegisterRoute(nameof(ArtistManagement), typeof(ArtistManagement));
            Routing.RegisterRoute(nameof(UserManagement), typeof(UserManagement));
            Routing.RegisterRoute(nameof(SongManagement), typeof(SongManagement));
            Routing.RegisterRoute(nameof(PublisherManagement), typeof(PublisherManagement));
            
            Routing.RegisterRoute(nameof(AddPublisher), typeof(AddPublisher));
            Routing.RegisterRoute(nameof(AddArtist), typeof(AddArtist));
            Routing.RegisterRoute(nameof(AddSong), typeof(AddSong));
            Routing.RegisterRoute(nameof(LandingPage), typeof(LandingPage));

        }
    }
}

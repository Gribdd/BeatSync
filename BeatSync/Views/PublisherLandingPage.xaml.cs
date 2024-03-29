
namespace BeatSync.Views;

public partial class PublisherLandingPage : Shell
{
	public PublisherLandingPage()
	{
		Routing.RegisterRoute($"songs/{nameof(AddSong)}", typeof(AddSong));
		Routing.RegisterRoute($"library/{nameof(AddAlbumPublisher)}", typeof(AddAlbumPublisher));
		Routing.RegisterRoute($"library/{nameof(AddAlbumSongs)}", typeof(AddAlbumSongs));

		BindingContext = this;
		InitializeComponent();
	}

    private async void OnProfileIconClicked(object sender, EventArgs e)
    {
        bool answer = await Shell.Current.DisplayAlert("Logout", "Would you like to log out?", "Yes", "No");
        if (answer)
        {
            Application.Current!.MainPage = new AppShell();
            Preferences.Default.Set("currentUserId", -1);
        }
    }
}
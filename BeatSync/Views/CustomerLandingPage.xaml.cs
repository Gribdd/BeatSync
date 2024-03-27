using BeatSync.Pages;

namespace BeatSync.Views;

public partial class CustomerLandingPage : Shell
{
	public CustomerLandingPage()
	{
		Routing.RegisterRoute($"library/{nameof(AddPlaylistCustomer)}",typeof(AddPlaylistCustomer));
		Routing.RegisterRoute($"library/{nameof(AddPlaylistSongsCustomer)}",typeof(AddPlaylistSongsCustomer));
		InitializeComponent();
	}
}
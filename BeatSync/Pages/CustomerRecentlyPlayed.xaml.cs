namespace BeatSync.Pages;

public partial class CustomerRecentlyPlayed : ContentPage
{
	public CustomerRecentlyPlayed(CustomerRecentlyPlayedViewModel vm)
	{
		InitializeComponent();
		BindingContext =  vm;
	}
}
namespace BeatSync.Pages;

public partial class PubRecentlyPlayed : ContentPage
{
	public PubRecentlyPlayed(PubRecentlyPlayedViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;

    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
    }
}
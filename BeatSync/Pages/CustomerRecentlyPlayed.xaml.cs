namespace BeatSync.Pages;

public partial class CustomerRecentlyPlayed : ContentPage
{
	CustomerRecentlyPlayedViewModel _vm;

    public CustomerRecentlyPlayed(CustomerRecentlyPlayedViewModel vm)
	{
		InitializeComponent();
		BindingContext = _vm = vm;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }
}
using BeatSync.ViewModel.Users;

namespace BeatSync.Pages;

public partial class LandingPage : ContentPage
{
	LandingPageViewModel _vm;
	public LandingPage(LandingPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = _vm = vm;
	}

	protected override void OnAppearing()
	{
        base.OnAppearing();
    }	
}
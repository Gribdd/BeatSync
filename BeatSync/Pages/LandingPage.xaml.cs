namespace BeatSync.Pages;

public partial class LandingPage : ContentPage
{
	LandingPageViewModel _vm;
	public LandingPage(LandingPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = _vm = vm;
	}

	protected async override void OnAppearing()
	{
        base.OnAppearing();
        await _vm.LoadSongsAsync();
        _vm.LoadCurrentUser();
        _vm.LoadSuggestedSongs();
        
    }	
}
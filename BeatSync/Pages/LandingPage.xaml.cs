
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
        var watch = System.Diagnostics.Stopwatch.StartNew();
        await _vm.LoadSongsAsync();
        await _vm.LoadCurrentUser();
        await _vm.LoadSuggestedSongs();
        await _vm.LoadRecentlyPlayedSongs();
        watch.Stop();
        var elapsedMs = watch.ElapsedMilliseconds;
        System.Diagnostics.Debug.WriteLine($"LandingPage.OnAppearing took {elapsedMs}ms");
    }	
}
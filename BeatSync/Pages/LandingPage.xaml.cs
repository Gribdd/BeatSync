
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
        //testing to see if this is faster than the commented out code below
        //var tasks = new List<Task>();
        //tasks.Add(_vm.LoadSongsAsync());
        //tasks.Add(_vm.LoadCurrentUser());
        //tasks.Add(_vm.LoadSuggestedSongs());
        //tasks.Add(_vm.LoadRecentlyPlayedSongs());
        //await Task.WhenAll(tasks).ConfigureAwait(false);
        await _vm.LoadSongsAsync();
        await _vm.LoadCurrentUser();
        await _vm.LoadSuggestedSongs();
        await _vm.LoadRecentlyPlayedSongs();
        watch.Stop();
        var elapsedMs = watch.ElapsedMilliseconds;
        System.Diagnostics.Debug.WriteLine($"LandingPage.OnAppearing took {elapsedMs}ms");
    }	
}
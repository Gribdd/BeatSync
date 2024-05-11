namespace BeatSync.Pages;

public partial class LibraryPage : ContentPage
{
	LibraryPageViewModel _vm;
	public LibraryPage(LibraryPageViewModel vm)
	{
		BindingContext = _vm =  vm;
		InitializeComponent();
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        var watch = System.Diagnostics.Stopwatch.StartNew();
        await _vm.GetAlbums();
		await _vm.LoadCurrentUser();
        watch.Stop();
        var elapsedMs = watch.ElapsedMilliseconds;
        System.Diagnostics.Debug.WriteLine($"LibraryPage.OnAppearing took {elapsedMs}ms");
    }
}
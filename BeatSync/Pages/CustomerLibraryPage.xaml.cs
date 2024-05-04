namespace BeatSync.Pages;

public partial class CustomerLibraryPage : ContentPage
{
	CustomerLibraryPageViewModel _vm;

    public CustomerLibraryPage(CustomerLibraryPageViewModel vm)
	{
		BindingContext = _vm = vm;
		InitializeComponent();
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await _vm.LoadCurrentUser();
        _vm.LoadRecentlyPlayedSongs();
        _vm.GetUserLikeSongs();
        _vm.LoadPlaylists();
    }
}
namespace BeatSync.Pages;

public partial class AlbumSearchPage : ContentPage
{
    AlbumSearchPageViewModel _vm;
    public AlbumSearchPage(AlbumSearchPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = _vm = vm;
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await _vm.GetAlbums();
    }
}
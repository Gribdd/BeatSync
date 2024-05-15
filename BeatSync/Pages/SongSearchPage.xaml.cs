namespace BeatSync.Pages;

public partial class SongSearchPage : ContentPage
{
    SongSearchPageViewModel _vm;
    public SongSearchPage(SongSearchPageViewModel vm)
	{
		InitializeComponent();
        BindingContext = _vm = vm;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await _vm.GetSongs();
    }
}
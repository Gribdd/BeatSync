namespace BeatSync.Pages;

public partial class AddSong : ContentPage
{
	AddSongViewModel _vm;

    public AddSong(AddSongViewModel vm)
	{
		InitializeComponent();
		BindingContext = _vm = vm;
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await _vm.PopulateArtist();
    }
}
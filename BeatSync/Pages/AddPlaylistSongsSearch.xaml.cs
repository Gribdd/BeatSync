namespace BeatSync.Pages;

public partial class AddPlaylistSongsSearch : ContentPage
{
	AddPlaylistSongsSearchViewModel _vm;
	public AddPlaylistSongsSearch(AddPlaylistSongsSearchViewModel vm)
	{
		BindingContext = _vm = vm;
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
		_vm.GetSongs();
    }
}
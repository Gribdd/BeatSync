using BeatSync.ViewModel.General;

namespace BeatSync.Pages;

public partial class AddAlbumSongs : ContentPage
{
	AddAlbumSongsViewModel _vm;
	public AddAlbumSongs(AddAlbumSongsViewModel vm)
	{
		BindingContext = _vm = vm;
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
		_vm.GetSongsByArtistId();
	}
}
using BeatSync.ViewModel.Admin;

namespace BeatSync.Pages;

public partial class AddSong : ContentPage
{
	AddSongViewModel _vm;

    public AddSong(AddSongViewModel vm)
	{
		InitializeComponent();
		BindingContext = _vm = vm;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _vm.PopulateArtist();
    }
}
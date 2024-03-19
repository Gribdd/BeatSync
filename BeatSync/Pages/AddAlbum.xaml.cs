using BeatSync.ViewModel.Admin;

namespace BeatSync.Pages;

public partial class AddAlbum : ContentPage
{
	public AddAlbum(AddAlbumViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}
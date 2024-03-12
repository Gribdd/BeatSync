using BeatSync.ViewModel.Admin;

namespace BeatSync.Pages;

public partial class AddArtist : ContentPage
{
	public AddArtist(AddArtistViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
	}


}
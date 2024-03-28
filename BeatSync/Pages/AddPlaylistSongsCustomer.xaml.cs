namespace BeatSync.Pages;

public partial class AddPlaylistSongsCustomer : ContentPage
{
	public AddPlaylistSongsCustomer(AddPlaylistSongsCustomerViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}
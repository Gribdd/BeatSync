
namespace BeatSync.Views;

public partial class CustomerLandingPage : Shell
{
	CustomerLandingPageViewModel _vm;
	public CustomerLandingPage(CustomerLandingPageViewModel vm)
	{
		Routing.RegisterRoute($"library/{nameof(AddPlaylistCustomer)}",typeof(AddPlaylistCustomer));
		Routing.RegisterRoute($"library/{nameof(AddPlaylistSongsCustomer)}",typeof(AddPlaylistSongsCustomer));
		Routing.RegisterRoute($"library/{nameof(AddPlaylistSongsCustomer)}/{nameof(AddPlaylistSongsSearch)}",typeof(AddPlaylistSongsSearch));

		BindingContext = _vm = vm;
		InitializeComponent(); 
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await _vm.GetActiveCustomer();
    }
}
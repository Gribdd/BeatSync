namespace BeatSync.Pages;

public partial class AddPlaylistCustomer : ContentPage
{
	AddPlaylistCustomerViewModel _vm;
    public AddPlaylistCustomer(AddPlaylistCustomerViewModel vm)
	{
		InitializeComponent();
		BindingContext = _vm = vm;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        _vm.LoadCurrentUser();
        await Task.Delay(200);
    }
}


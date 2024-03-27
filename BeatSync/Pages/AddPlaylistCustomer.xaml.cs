using BeatSync.ViewModel.Users;

namespace BeatSync.Pages;

public partial class AddPlaylistCustomer : ContentPage
{
	AddPlaylistCustomerViewModel _vm;
    public AddPlaylistCustomer(AddPlaylistCustomerViewModel vm)
	{
		InitializeComponent();
		BindingContext = _vm = vm;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _vm.LoadCurrentUser();
    }
}


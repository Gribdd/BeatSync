namespace BeatSync.Pages;

public partial class AddPlaylistSongsCustomer : ContentPage
{
	AddPlaylistSongsCustomerViewModel _vm;

    public AddPlaylistSongsCustomer(AddPlaylistSongsCustomerViewModel vm)
	{
		BindingContext = _vm = vm;
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _vm.GetPlaylistSongsPlaylistId();
        await _vm.GetSongsByPlaylistId();
        await Task.Delay(200);
        await _vm.GetUserLikeSongs();
    }
}
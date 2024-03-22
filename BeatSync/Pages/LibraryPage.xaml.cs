using BeatSync.ViewModel.PublisherShell;

namespace BeatSync.Pages;

public partial class LibraryPage : ContentPage
{
	LibraryPageViewModel _vm;
	public LibraryPage(LibraryPageViewModel vm)
	{
		BindingContext = _vm =  vm;
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
		_vm.GetAlbums();
    }
}
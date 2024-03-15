using BeatSync.ViewModel.PublisherShell;

namespace BeatSync.Pages;

public partial class LibraryPage : ContentPage
{
	public LibraryPage(LibraryPageViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}
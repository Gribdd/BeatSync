using BeatSync.ViewModel.Admin;

namespace BeatSync.Pages;

public partial class AddPublisher : ContentPage
{
	public AddPublisher(AddPublisherViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}
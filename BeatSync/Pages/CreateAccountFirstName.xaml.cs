namespace BeatSync.Pages;

public partial class CreateAccountFirstName : ContentPage
{
	public CreateAccountFirstName(CreateAccountFirstNameViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}

}
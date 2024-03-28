namespace BeatSync.Pages;

public partial class AddUser : ContentPage
{
	public AddUser(AddUserViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}
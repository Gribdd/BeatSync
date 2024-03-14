using BeatSync.ViewModel.LoginAndRegistration;

namespace BeatSync.Pages;

public partial class CreateAccountUsername : ContentPage
{
	public CreateAccountUsername(CreateAccountUsernameViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}

using BeatSync.ViewModel.LoginAndRegistration;

namespace BeatSync.Pages;

public partial class CreateAccountPassword : ContentPage
{
	public CreateAccountPassword(CreateAccountPasswordViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}
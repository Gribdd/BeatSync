
namespace BeatSync.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginPageViewModel vm)
	{
        BindingContext = vm;
		InitializeComponent();
	}

}
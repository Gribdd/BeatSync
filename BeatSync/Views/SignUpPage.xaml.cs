
using BeatSync.ViewModel.LoginAndRegistration;

namespace BeatSync.Views;

public partial class SignUpPage : ContentPage
{
	public SignUpPage(SignUpPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}

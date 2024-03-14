
using BeatSync.ViewModel.LoginAndRegistration;

namespace BeatSync.Pages;

public partial class CreateAccountGender : ContentPage
{
	public CreateAccountGender(CreateAccountGenderViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}

}
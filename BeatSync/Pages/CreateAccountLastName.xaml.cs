
using BeatSync.ViewModel.LoginAndRegistration;

namespace BeatSync.Pages;

public partial class CreateAccountLastName : ContentPage
{
	public CreateAccountLastName(CreateAccountLastNameViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}

}
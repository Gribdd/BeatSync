
using BeatSync.ViewModel.LoginAndRegistration;

namespace BeatSync.Pages;

public partial class CreateAccountDOB : ContentPage
{
	public CreateAccountDOB(CreateAccountDOBViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}

   
}
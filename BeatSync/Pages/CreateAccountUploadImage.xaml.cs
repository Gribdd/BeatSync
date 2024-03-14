
using BeatSync.ViewModel.LoginAndRegistration;

namespace BeatSync.Pages;
public partial class CreateAccountUploadImage : ContentPage
{
	public CreateAccountUploadImage(CreateAccountUploadImageViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}
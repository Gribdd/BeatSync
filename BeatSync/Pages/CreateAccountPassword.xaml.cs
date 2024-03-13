using BeatSync.Views;

namespace BeatSync.Pages;

public partial class CreateAccountPassword : ContentPage
{
	public CreateAccountPassword()
	{
		InitializeComponent();
	}

    private void OnBtnReturn_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new SignUpPage());
    }

    private void OnBtnNext_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new CreateAccountDOB());
    }
}
using BeatSync.Pages;

namespace BeatSync.Views;

public partial class SignUpPage : ContentPage
{
	public SignUpPage()
	{
		InitializeComponent();
	}

    private void OnBtnReturn_Clicked(object sender, EventArgs e)
    {
		Navigation.PushAsync(new MainPage());	
    }

	private void OnBtnNext_Clicked(object sender, EventArgs e)
	{
		Navigation.PushAsync(new CreateAccountPassword());
	}
}
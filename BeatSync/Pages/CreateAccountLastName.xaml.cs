namespace BeatSync.Pages;

public partial class CreateAccountLastName : ContentPage
{
	public CreateAccountLastName()
	{
		InitializeComponent();
	}

    private void OnBtnReturn_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new CreateAccountFirstName());
    }

    private void OnBtnNext_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new CreateAccountGender());
    }
}
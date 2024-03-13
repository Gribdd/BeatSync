namespace BeatSync.Pages;

public partial class CreateAccountFirstName : ContentPage
{
	public CreateAccountFirstName()
	{
		InitializeComponent();
	}

    private void OnBtnReturn_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new CreateAccountDOB());
    }

    private void OnBtnNext_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new CreateAccountLastName());
    }
}
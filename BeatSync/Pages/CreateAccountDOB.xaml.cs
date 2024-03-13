namespace BeatSync.Pages;

public partial class CreateAccountDOB : ContentPage
{
	public CreateAccountDOB()
	{
		InitializeComponent();
	}

    private void OnBtnReturn_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new CreateAccountPassword());
    }

    private void OnBtnNext_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new CreateAccountFirstName());
    }
}
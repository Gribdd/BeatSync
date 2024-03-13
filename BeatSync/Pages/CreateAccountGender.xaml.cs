using BeatSync.Views;

namespace BeatSync.Pages;

public partial class CreateAccountGender : ContentPage
{
	public CreateAccountGender()
	{
		InitializeComponent();
	}

    private void OnBtnReturn_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new CreateAccountLastName());
    }

    private async void OnBtnNext_Clicked(object sender, EventArgs e)
    {
        await DisplayAlert("Success", "Account created!", "OK");
        //await Shell.Current.GoToAsync($"{nameof(CustomerLandingPage)}");
        Application.Current.MainPage = new CustomerLandingPage();
    }
}
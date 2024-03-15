using BeatSync.Pages;

namespace BeatSync.Views;

public partial class Admin_LoginPage : ContentPage
{
	public Admin_LoginPage()
	{
		InitializeComponent();
	}

    private async void OnlblSignInTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(MainPage)}");
    }

    private async void OnbtnLogin_Clicked(object sender, EventArgs e)
    {
        string username = "admin";
        string password = "admin123";

        if(username == txtEmail.Text && password == txtPassword.Text)
        {
            //Application.Current.MainPage = new NavigationPage(new Admin_LandingPage());
            //await Shell.Current.GoToAsync($"{nameof(SongManagement)}");
            Application.Current!.MainPage = new Admin_LandingPage();

        }
        else
        {
            await DisplayAlert("Error", "Invalid username or password", "Ok");
        }
    }
}
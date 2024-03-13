namespace BeatSync.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}
    private async void OnlblSignInTapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }

    private void OnbtnLogin_Clicked(object sender, EventArgs e)
    {

        //add navigation

        /*
        if(username == txtEmail.Text && password == txtPassword.Text)
        {
            //Application.Current.MainPage = new NavigationPage(new Admin_LandingPage());
            //await Shell.Current.GoToAsync($"{nameof(SongManagement)}");
            Application.Current.MainPage = new Admin_LandingPage();

        }
        else
        {
            await DisplayAlert("Error", "Invalid username or password", "Ok");
        }
        */
    }

}
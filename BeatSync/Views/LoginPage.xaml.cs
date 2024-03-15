using BeatSync.ViewModel.LoginAndRegistration;

namespace BeatSync.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginPageViewModel vm)
	{
        BindingContext = vm;
		InitializeComponent();
	}


    private void OnbtnLogin_Clicked(object sender, EventArgs e)
    {
        //temporary code to test navigation 
        //will navigate to publisher landing page

        Application.Current!.MainPage = new PublisherLandingPage();


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
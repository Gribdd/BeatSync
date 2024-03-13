using BeatSync.Views;

namespace BeatSync
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnlblLoginTapped(object sender, TappedEventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
        }

        private async void OnlblLoginAdminTapped(object sender, TappedEventArgs e)
        {
            //await Navigation.PushAsync(new Admin_LoginPage());
            await Shell.Current.GoToAsync($"{nameof(Admin_LoginPage)}");
        }

        private async void OnbtnSignAsCustomer_Clicked(object sender, EventArgs e)
        {
            //set user type to 3 
            await Shell.Current.GoToAsync($"{nameof(SignUpPage)}");
        }

        private async void OnbtnSignAsPublisher_Clicked(object sender, EventArgs e)
        {
            //set user type to  2
            await Shell.Current.GoToAsync($"{nameof(SignUpPage)}");
        }

        private async void OnbtnSignAsArtist_Clicked(object sender, EventArgs e)
        {
            //set user type to 1
            await Shell.Current.GoToAsync($"{nameof(SignUpPage)}");
        }
    }

}

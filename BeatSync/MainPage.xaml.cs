using BeatSync.Views;

namespace BeatSync
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnlblLoginTapped(object sender, TappedEventArgs e)
        {
            

        }

        private async void OnlblLoginAdminTapped(object sender, TappedEventArgs e)
        {
            await Navigation.PushAsync(new Admin_LoginPage());
        }
    }

}

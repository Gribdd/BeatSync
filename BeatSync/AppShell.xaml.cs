using BeatSync.Pages;
using BeatSync.Views;

namespace BeatSync
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {

            Routing.RegisterRoute(nameof(Admin_LoginPage), typeof(Admin_LoginPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            //mainpage is already registered in AppShell.xaml, do not declare twice
            //creating route hierarchy
            //Routing.RegisterRoute(nameof(Admin_LandingPage), typeof(Admin_LandingPage));
            //Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            //Routing.RegisterRoute(nameof(CustomerLandingPage), typeof(CustomerLandingPage));
            Routing.RegisterRoute("mainpage/signup", typeof(SignUpPage));
            Routing.RegisterRoute("mainpage/signup/createaccountusername", typeof(CreateAccountUsername));
            Routing.RegisterRoute("mainpage/signup/createaccountusername/createaccountpassword", typeof(CreateAccountPassword));
            Routing.RegisterRoute("mainpage/signup/createaccountusername/createaccountpassword/createaccountdob", typeof(CreateAccountDOB));
            Routing.RegisterRoute("mainpage/signup/createaccountusername/createaccountpassword/createaccountdob/createaccountfirstname", typeof(CreateAccountFirstName));
            Routing.RegisterRoute("mainpage/signup/createaccountusername/createaccountpassword/createaccountdob/createaccountfirstname/createaccountlastname", typeof(CreateAccountLastName));
            Routing.RegisterRoute("mainpage/signup/createaccountusername/createaccountpassword/createaccountdob/createaccountfirstname/createaccountlastname/createaccountgender", typeof(CreateAccountGender));
            Routing.RegisterRoute("mainpage/signup/createaccountusername/createaccountpassword/createaccountdob/createaccountfirstname/createaccountlastname/createaccountgender/createaccountuploadimage", typeof(CreateAccountUploadImage));
            InitializeComponent();
        }
    }
}

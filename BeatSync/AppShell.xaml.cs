namespace BeatSync;

public partial class AppShell : Shell
{
    public AppShell()
    {

        Routing.RegisterRoute(nameof(Admin_LoginPage), typeof(Admin_LoginPage));
        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        
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

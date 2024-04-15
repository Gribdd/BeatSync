namespace BeatSync.ViewModel.LoginAndRegistration;

public partial class MainPageViewModel : ObservableObject
{
    [ObservableProperty]
    private User _user = new();
   
    [RelayCommand]
    async Task SignUpCustomer()
    {
        User.AccountType = 3;
        var navigationParameter = new Dictionary<string, object>
        {
            {nameof(User), User }
        };
        await Shell.Current.GoToAsync("signup",navigationParameter);
    }

    [RelayCommand]
    async Task SignUpPublisher()
    {
        User.AccountType = 2;
        var navigationParameter = new Dictionary<string, object>
        {
            {nameof(User), User }
        };
        await Shell.Current.GoToAsync("signup", navigationParameter);
    }

    [RelayCommand]
    async Task SignUpArtist()
    {
        User.AccountType = 1;
        var navigationParameter = new Dictionary<string, object>
        {
            {nameof(User), User }
        };
        await Shell.Current.GoToAsync("signup", navigationParameter);
    }

    [RelayCommand]
    async Task Login()
    {
        await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
    }

    [RelayCommand]
    async Task LoginAdmin()
    {
        await Shell.Current.GoToAsync($"{nameof(Admin_LoginPage)}");
    }
}

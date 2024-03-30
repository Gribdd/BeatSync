
namespace BeatSync.ViewModel.Users;

public partial class CustomerLandingPageViewModel : ObservableObject
{
    private readonly UserService userService;

    [ObservableProperty]
    private User _user = new();

    public CustomerLandingPageViewModel(UserService userService)
    {
        this.userService = userService;
    }

    [RelayCommand]
    async Task OnProfileIconClicked()
    {
        bool answer = await Shell.Current.DisplayAlert("Logout", "Would you like to log out?", "Yes", "No");
        if (answer)
        {
            Application.Current!.MainPage = new AppShell();
            Preferences.Default.Set("currentUserId", -1);
        }
    }

    public async Task GetActiveCustomer()
    {
        User = await userService.GetCurrentUser();
    }
}

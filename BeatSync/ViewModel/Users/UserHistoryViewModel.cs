namespace BeatSync.ViewModel.Users;

public partial class UserHistoryViewModel : ObservableObject
{
	private UserService userService;

	[ObservableProperty]
	private User _user = new();

	public UserHistoryViewModel(UserService userService)
	{
		this.userService = userService;
	}

	[RelayCommand]
	void Logout()
	{
        Shell.Current.FlyoutIsPresented = !Shell.Current.FlyoutIsPresented;
    }

	public async void LoadCurrentUser()
	{
		User = await userService.GetCurrentUser();
	}
}
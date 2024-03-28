namespace BeatSync.ViewModel.Users;

public partial class UserHistoryViewModel : ObservableObject
{
	private AdminService _adminService;

	//will double check if needed
	//[ObservableProperty]
	//private ObservableCollection<UserHistory> _userHistories = new();

	public UserHistoryViewModel(AdminService adminService)
	{
		_adminService = adminService;
	}

	[RelayCommand]
	async Task Logout()
	{
		await _adminService.Logout();
	}
}
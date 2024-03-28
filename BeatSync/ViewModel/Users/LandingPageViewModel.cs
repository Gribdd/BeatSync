namespace BeatSync.ViewModel.Users;

public partial class LandingPageViewModel : ObservableObject
{
	private AdminService _adminService;
	public LandingPageViewModel(AdminService adminService)
	{
		_adminService = adminService;
	}

	[RelayCommand]
	async Task Logout()
	{
        await _adminService.Logout();
    }
}
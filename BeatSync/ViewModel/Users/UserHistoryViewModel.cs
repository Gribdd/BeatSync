using BeatSync.Models;
using BeatSync.Pages;
using BeatSync.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;


namespace BeatSync.ViewModel.Users;

public partial class UserHistoryViewModel : ObservableObject
{
	private AdminService _adminService;

	[ObservableProperty]
	private ObservableCollection<UserHistory> _userHistories = new();

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
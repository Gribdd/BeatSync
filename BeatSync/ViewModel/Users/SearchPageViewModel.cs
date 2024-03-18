using BeatSync.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BeatSync.ViewModel.Users;

public partial class SearchPageViewModel : ObservableObject
{
	private AdminService _adminService; 
	public SearchPageViewModel(AdminService adminService)
	{
		_adminService = adminService;
	}

	[RelayCommand]
	async Task Logout()
	{
        await _adminService.Logout();
    }
}
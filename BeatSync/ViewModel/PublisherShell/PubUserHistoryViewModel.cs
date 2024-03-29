using BeatSync.Models;
using BeatSync.Pages;
using BeatSync.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Linq;


namespace BeatSync.ViewModel.PublisherShell;

public partial class PubUserHistoryViewModel : ObservableObject
{

	private AdminService _adminService;
	private PublisherService _publisherService;

    [ObservableProperty]
    private ObservableCollection<History> _userHistories = new();

	[ObservableProperty]
	private ObservableCollection<History> _filteredHistories = new();

    public PubUserHistoryViewModel(AdminService adminService, PublisherService publisherService)
	{
		_adminService = adminService;
		_publisherService = publisherService;
	}


	[RelayCommand]
	async Task Logout()
	{
        await _adminService.Logout();
    }

    [RelayCommand]
    public async Task GetUserHistories()
    {
        var currentUser = await _publisherService.GetCurrentUser();
        UserHistories = await _publisherService.LoadUserHistoriesAsync();
        FilteredHistories = new ObservableCollection<History>(UserHistories.Where(history => history.UserId == currentUser.Id));
    }


}


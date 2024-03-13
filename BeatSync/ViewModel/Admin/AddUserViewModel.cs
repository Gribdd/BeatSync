using BeatSync.Models;
using BeatSync.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BeatSync.ViewModel.Admin
{
    public partial class AddUserViewModel: ObservableObject
    {
        private AdminService _adminService;

        [ObservableProperty]
        private User _user = new();

        public AddUserViewModel(AdminService adminService)
        {
            _adminService = adminService;
        }

        [RelayCommand]
        async Task AddUser()
        {
            if(await _adminService.AddUserAsync(User))
            {
                await Shell.Current.DisplayAlert("Add User", "User successfully added", "OK");
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                await Shell.Current.DisplayAlert("Add User", "Please enter all fields", "OK");
            }
        }

        [RelayCommand]
        async Task Return()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}

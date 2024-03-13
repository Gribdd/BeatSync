using BeatSync.Models;
using BeatSync.Pages;
using BeatSync.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BeatSync.ViewModel.Admin
{
    public partial class UserManagementViewModel: ObservableObject
    {
        private AdminService _adminService;

        [ObservableProperty]
        private ObservableCollection<User> _users = new();

        public UserManagementViewModel(AdminService adminService)
        {
            _adminService = adminService;
        }

        [RelayCommand]
        async Task NavigateAddUser()
        {
            await Shell.Current.GoToAsync($"{nameof(AddUser)}");
        }

        [RelayCommand]
        async Task DeleteUser()
        {
            string inputId = await Shell.Current.DisplayPromptAsync("Delete User", "Enter User ID to delete:");
            if (!string.IsNullOrEmpty(inputId) && int.TryParse(inputId, out int id))
            {
                Users = await _adminService.DeleteUserAsync(id);
            }

        }

        [RelayCommand]
        async Task UpdateUser()
        {
            string inputId = await Shell.Current.DisplayPromptAsync("Update User", "Enter User ID to delete:");
            if (!string.IsNullOrEmpty(inputId) && int.TryParse(inputId, out int id))
            {

                Users = await _adminService.UpdateUserAsync(id);
            }
        }

        public ICommand GetUsersCommand => new Command(GetUsers);

        public async void GetUsers()
        {
            Users = await _adminService.GetActiveUserAsync();
        }
    }
}

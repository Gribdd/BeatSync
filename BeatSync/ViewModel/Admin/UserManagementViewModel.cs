
using BeatSync.Services.Service;

namespace BeatSync.ViewModel.Admin;

public partial class UserManagementViewModel : ObservableObject
{
    private UserService _userService;

    [ObservableProperty]
    private ObservableCollection<User> _users = new();

    public UserManagementViewModel(UserService userService)
    {
        _userService = userService;
    }

    [RelayCommand]
    async Task NavigateAddUser()
    {
        await Shell.Current.GoToAsync($"{nameof(AddUser)}");
    }

    [RelayCommand]
    async Task DeleteUser()
    {
        string username = await Shell.Current.DisplayPromptAsync("Delete User", "Enter User username to delete:");
        if (!string.IsNullOrEmpty(username))
        {
            var user = await _userService.GetByUsernameAsync(username);
            await _userService.DeleteAsync(user.Id);
        }
        Users = await _userService.GetActiveAsync();

    }

    [RelayCommand]
    async Task UpdateUser()
    {
        string username = await Shell.Current.DisplayPromptAsync("Update User", "Enter User username to update:");
        if (!string.IsNullOrEmpty(username))
        {
            var user = await _userService.GetByUsernameAsync(username);
            await _userService.UpdateAsync(user.Id);
        }
        Users = await _userService.GetActiveAsync();
    }

    [RelayCommand]
    async Task Logout()
    {
        bool answer = await Shell.Current.DisplayAlert("Logout", "Would you like to log out?", "Yes", "No");
        if (answer)
        {
            Application.Current!.MainPage = new AppShell();
            Preferences.Default.Set("currentUserId", -1);
        }
    }

    public async void GetUsers()
    {
        Users = await _userService.GetActiveAsync();
    }

    [RelayCommand]
    async Task NavigateToSearch()
    {
        await Shell.Current.GoToAsync(nameof(AdminSearchPage));
    }

    [RelayCommand]
    async Task Return()
    {
        await Shell.Current.GoToAsync("..");
    }
}

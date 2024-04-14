
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
        string inputId = await Shell.Current.DisplayPromptAsync("Delete User", "Enter User ID to delete:");
        if (!string.IsNullOrEmpty(inputId) && int.TryParse(inputId, out int id))
        {
            await _userService.DeleteAsync(id);
        }
        Users = await _userService.GetActiveAsync();

    }

    [RelayCommand]
    async Task UpdateUser()
    {
        string inputId = await Shell.Current.DisplayPromptAsync("Update User", "Enter User ID to update:");
        if (!string.IsNullOrEmpty(inputId) && int.TryParse(inputId, out int id))
        {
            await _userService.UpdateAsync(id);
        }
        Users = await _userService.GetActiveAsync();
    }

    [RelayCommand]
    async Task Logout()
    {
        Preferences.Default.Set("currentUserId", -1);
        Application.Current!.MainPage = new AppShell();
    }

    public async void GetUsers()
    {
        Users = await _userService.GetActiveAsync();
    }
}

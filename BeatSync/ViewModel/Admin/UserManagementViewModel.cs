
using BeatSync.Repositories;

namespace BeatSync.ViewModel.Admin;

public partial class UserManagementViewModel : ObservableObject
{
    private UserService userService;
    private readonly IRepository<User> _userRepository;

    [ObservableProperty]
    private ObservableCollection<User> _users = new();

    public UserManagementViewModel(UserService userService, IRepository<User> userRepository)
    {
        this.userService = userService;
        _userRepository = userRepository;
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
            Users = await userService.DeleteUserAsync(id);
        }

    }

    [RelayCommand]
    async Task UpdateUser()
    {
        string inputId = await Shell.Current.DisplayPromptAsync("Update User", "Enter User ID to delete:");
        if (!string.IsNullOrEmpty(inputId) && int.TryParse(inputId, out int id))
        {

            Users = await userService.UpdateUserAsync(id);
        }
    }

    [RelayCommand]
    async Task Logout()
    {

    }

    public async void GetUsers()
    {
        Users = await _userRepository.GetActive();
    }
}



using BeatSync.Models;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace BeatSync.Services;

public class UserService
{
    private readonly string userFilePath = Path.Combine(FileSystem.Current.AppDataDirectory, "Users.json");
    public async Task<bool> AddUserAsync(User user)
    {
        if (user == null)
        {
            return false;
        }

        ObservableCollection<User> users = await GetUsersAsync();

        user.Id = users.Count + 1;
        user.AccounType = 3;
        user.IsDeleted = false;

        users.Add(user);

        var json = JsonSerializer.Serialize<ObservableCollection<User>>(users);
        await File.WriteAllTextAsync(userFilePath, json);
        return true;
    }

    public async Task<ObservableCollection<User>> GetUsersAsync()
    {
        if (!File.Exists(userFilePath))
        {
            return new ObservableCollection<User>();
        }

        var json = await File.ReadAllTextAsync(userFilePath);
        var users = JsonSerializer.Deserialize<ObservableCollection<User>>(json);
        return users!;
    }

    public async Task<ObservableCollection<User>> GetActiveUserAsync()
    {
        var users = await GetUsersAsync();
        return new ObservableCollection<User>(users.Where(m => !m.IsDeleted));
    }

    public async Task<ObservableCollection<User>> DeleteUserAsync(int id)
    {
        var users = await GetUsersAsync();
        var userToBeDeleted = users.FirstOrDefault(m => m.Id == id);
        if (userToBeDeleted == null)
        {
            await Shell.Current.DisplayAlert("Error", "User not found", "OK");
            return users;
        }

        if (userToBeDeleted.IsDeleted)
        {
            await Shell.Current.DisplayAlert("Error", "User already deleted", "OK");
            return users;
        }

        userToBeDeleted.IsDeleted = true;
        var json = JsonSerializer.Serialize<ObservableCollection<User>>(users);
        await File.WriteAllTextAsync(userFilePath, json);

        users.Remove(userToBeDeleted);
        await Shell.Current.DisplayAlert("Delete User", "Successfully deleted user", "OK");
        return await GetActiveUserAsync();
    }

    public async Task<ObservableCollection<User>> UpdateUserAsync(int id)
    {
        var users = await GetUsersAsync();
        var userToBeUpdated = users.FirstOrDefault(m => m.Id == id);
        if (userToBeUpdated == null)
        {
            await Shell.Current.DisplayAlert("Error", "User not found", "OK");
            return users;
        }

        string[] editOptions = { "Email", "Username", "Password", "First Name", "Last Name" };
        string selectedOption = await Shell.Current.DisplayActionSheet("Select Property to Edit", "Cancel", null, editOptions);

        var newValue = string.Empty;
        for (int index = 0; index < editOptions.Length; index++)
        {
            if (editOptions[index] == selectedOption)
            {
                switch (index)
                {
                    case 0: // Email
                        newValue = await Shell.Current.DisplayPromptAsync($"Edit User {selectedOption}", $"Enter new {selectedOption}:", initialValue: userToBeUpdated.Email);
                        userToBeUpdated.Email = newValue;
                        break;
                    case 1: // Username
                        newValue = await Shell.Current.DisplayPromptAsync($"Edit User {selectedOption}", $"Enter new {selectedOption}:", initialValue: userToBeUpdated.Username);
                        userToBeUpdated.Username = newValue;
                        break;
                    case 2: // Password
                        newValue = await Shell.Current.DisplayPromptAsync($"Edit User {selectedOption}", $"Enter new {selectedOption}:", initialValue: userToBeUpdated.Password);
                        userToBeUpdated.Password = newValue;
                        break;
                    case 3: // first name
                        newValue = await Shell.Current.DisplayPromptAsync($"Edit User {selectedOption}", $"Enter new {selectedOption}:", initialValue: userToBeUpdated.FirstName);
                        userToBeUpdated.FirstName = newValue;
                        break;
                    case 4: // last name
                        newValue = await Shell.Current.DisplayPromptAsync($"Edit User {selectedOption}", $"Enter new {selectedOption}:", initialValue: userToBeUpdated.LastName);
                        userToBeUpdated.LastName = newValue;
                        break;
                    default:
                        break;
                }
                break;
            }
        }

        int count = users.ToList().FindIndex(m => m.Id == id);
        users[count] = userToBeUpdated;
        var json = JsonSerializer.Serialize<ObservableCollection<User>>(users);
        await File.WriteAllTextAsync(userFilePath, json);

        await Shell.Current.DisplayAlert("Update User", "Successfully updated user", "OK");
        return await GetActiveUserAsync();
    }

    public async Task<User> GetCurrentUser()
    {
        int userId = Preferences.Default.Get("currentUserId", -1);
        var users = await GetActiveUserAsync();
        var user = users.FirstOrDefault(u => u.Id == userId);

        return user!;
    }
}

using BeatSync.Services.IService;

namespace BeatSync.Services.Service;

public class UserService : GenericService<User>, IUserService
{
    private readonly IUnitofWork _unitofWork;
    public UserService(IUnitofWork unitofWork) : base(unitofWork)
    {
        _unitofWork = unitofWork;
    }

    public async Task<User> GetByNameAsync(string name)
    {
        return await _unitofWork.UserRepository.GetByName(name);
    }

    public async Task<User> GetByUserNameAsync(string userName)
    {
        return await _unitofWork.UserRepository.GetByUserName(userName);
    }

    public async Task<User> GetCurrentUser()
    {
        int userId = Preferences.Default.Get("currentUserId", -1);
        var users = await GetActiveAsync();
        var user = GetAsync(userId).Result;

        return user!;
    }

    public override async Task UpdateAsync(int id)
    {
        var userToBeUpdated = await GetAsync(id);
        string[] editOptions = { "Email", "Username", "Password", "First Name", "Last Name" };
        string selectedOption = await Shell.Current.DisplayActionSheet("Select Property to Edit", "Cancel", null, editOptions);

        if (string.IsNullOrEmpty(selectedOption))
            return;

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

        await base.UpdateAsync(userToBeUpdated);
    }
}

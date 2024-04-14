using BeatSync.Services.Service;

namespace BeatSync.Services;

public class PublisherService : GenericService<Publisher>
{
    private readonly string _historyFilePath = Path.Combine(FileSystem.Current.AppDataDirectory, "PubUserHistory.json");

    public PublisherService(IUnitofWork unitofWork) : base(unitofWork)
    {
    }


    public override async Task UpdateAsync(int id)
    {
        var publisherToBeUpdated = await GetAsync(id);
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
                        newValue = await Shell.Current.DisplayPromptAsync($"Edit Artist {selectedOption}", $"Enter new {selectedOption}:", initialValue: publisherToBeUpdated.Email);
                        publisherToBeUpdated.Email = newValue;
                        break;
                    case 1: // Username
                        newValue = await Shell.Current.DisplayPromptAsync($"Edit Artist {selectedOption}", $"Enter new {selectedOption}:", initialValue: publisherToBeUpdated.Username);
                        publisherToBeUpdated.Username = newValue;
                        break;
                    case 2: // Password
                        newValue = await Shell.Current.DisplayPromptAsync($"Edit Artist {selectedOption}", $"Enter new {selectedOption}:", initialValue: publisherToBeUpdated.Password);
                        publisherToBeUpdated.Password = newValue;
                        break;
                    case 3: // first name
                        newValue = await Shell.Current.DisplayPromptAsync($"Edit Artist {selectedOption}", $"Enter new {selectedOption}:", initialValue: publisherToBeUpdated.FirstName);
                        publisherToBeUpdated.FirstName = newValue;
                        break;
                    case 4: // last name
                        newValue = await Shell.Current.DisplayPromptAsync($"Edit Artist {selectedOption}", $"Enter new {selectedOption}:", initialValue: publisherToBeUpdated.LastName);
                        publisherToBeUpdated.LastName = newValue;
                        break;
                    default:
                        break;
                }
                break;
            }
        }
        await base.UpdateAsync(publisherToBeUpdated);
    }

    public async Task<Publisher> GetCurrentUser()
    {
        int userId = Preferences.Default.Get("currentUserId", -1);
        var publishers = await GetActiveAsync();
        var publisher = publishers.FirstOrDefault(p => p.Id == userId);

        return publisher!;
    }

    public async Task SaveUserHistoryAsync(History history)
    {
        ObservableCollection<History> userHistories = await LoadUserHistoriesAsync();
        history.Id = userHistories.Count + 1;
        userHistories.Add(history);
        string json = JsonSerializer.Serialize<ObservableCollection<History>>(userHistories);
        await File.WriteAllTextAsync(_historyFilePath, json);
    }

    public async Task<ObservableCollection<History>> LoadUserHistoriesAsync()
    {
        if (!File.Exists(_historyFilePath))
        {
            return new ObservableCollection<History>();
        }

        var json = await File.ReadAllTextAsync(_historyFilePath);
        var userHistories = JsonSerializer.Deserialize<ObservableCollection<History>>(json);
        return userHistories!;
    }
}

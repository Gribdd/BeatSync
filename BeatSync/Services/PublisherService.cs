
﻿namespace BeatSync.Services;


public partial class PublisherService
{
    private readonly string _publisherFilePath = Path.Combine(FileSystem.Current.AppDataDirectory, "Publishers.json");
    private readonly string _historyFilePath = Path.Combine(FileSystem.Current.AppDataDirectory, "PubUserHistory.json");
    public async Task<bool> AddPublisherAsync(Publisher publisher)
    {

        if (publisher == null)
        {
            return false;
        }

        ObservableCollection<Publisher> publishers = await GetPublishersAsync();

        publisher.Id = publishers.Count + 1;
        publisher.AccounType = 2;
        publisher.IsDeleted = false;

        publishers.Add(publisher);

        var json = JsonSerializer.Serialize<ObservableCollection<Publisher>>(publishers);
        await File.WriteAllTextAsync(_publisherFilePath, json);
        return true;

    }

    public async Task<ObservableCollection<Publisher>> GetPublishersAsync()
    {
        if (!File.Exists(_publisherFilePath))
        {
            return new ObservableCollection<Publisher>();
        }

        var json = await File.ReadAllTextAsync(_publisherFilePath);
        var publishers = JsonSerializer.Deserialize<ObservableCollection<Publisher>>(json);
        return publishers!;
    }

    public async Task<ObservableCollection<Publisher>> GetActivePublisherAsync()
    {
        var publishers = await GetPublishersAsync();
        return new ObservableCollection<Publisher>(publishers.Where(m => !m.IsDeleted));
    }

    public async Task<ObservableCollection<Publisher>> DeletePublisherAsync(int id)
    {
        var publishers = await GetPublishersAsync();
        var publisherToBeDeleted = publishers.FirstOrDefault(m => m.Id == id);
        if (publisherToBeDeleted == null)
        {
            await Shell.Current.DisplayAlert("Error", "Publisher not found", "OK");
            return publishers;
        }

        if (publisherToBeDeleted.IsDeleted)
        {
            await Shell.Current.DisplayAlert("Error", "Publisher already deleted", "OK");
            return publishers;
        }

        publisherToBeDeleted.IsDeleted = true;
        var json = JsonSerializer.Serialize<ObservableCollection<Publisher>>(publishers);
        await File.WriteAllTextAsync(_publisherFilePath, json);

        publishers.Remove(publisherToBeDeleted);
        await Shell.Current.DisplayAlert("Delete Publisher", "Successfully deleted publisher", "OK");
        return await GetActivePublisherAsync();
    }

    public async Task<ObservableCollection<Publisher>> UpdatePublisherAsync(int id)
    {
        var publishers = await GetPublishersAsync();
        var publisherToBeUpdated = publishers.FirstOrDefault(m => m.Id == id);
        if (publisherToBeUpdated == null)
        {
            await Shell.Current.DisplayAlert("Error", "Publisher not found", "OK");
            return publishers;
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

        int count = publishers.ToList().FindIndex(m => m.Id == id);
        publishers[count] = publisherToBeUpdated;
        var json = JsonSerializer.Serialize<ObservableCollection<Publisher>>(publishers);
        await File.WriteAllTextAsync(_publisherFilePath, json);

        await Shell.Current.DisplayAlert("Update Publisher", "Successfully updated publisher", "OK");
        return await GetActivePublisherAsync();
    }

    public async Task<Publisher> GetCurrentUser()
    {
        int userId = Preferences.Default.Get("currentUserId", -1);
        var publishers = await GetActivePublisherAsync();
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

    [RelayCommand]
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

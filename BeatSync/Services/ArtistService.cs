using BeatSync.Models;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace BeatSync.Services;

public class ArtistService
{
    private readonly string _artistFilePath = Path.Combine(FileSystem.Current.AppDataDirectory, "Artists.json");

    public async Task<bool> AddArtistAsync(Artist artist)
    {

        if (artist == null)
        {
            return false;
        }

        ObservableCollection<Artist> artists = await GetArtistsAsync();

        artist.Id = artists.Count + 1;
        artist.AccounType = 1;
        artist.IsDeleted = false;

        artists.Add(artist);

        var json = JsonSerializer.Serialize<ObservableCollection<Artist>>(artists);
        await File.WriteAllTextAsync(_artistFilePath, json);
        return true;

    }

    public async Task<ObservableCollection<Artist>> DeleteArtistAsync(int id)
    {
        var artists = await GetArtistsAsync();
        var artistToBeDeleted = artists.FirstOrDefault(m => m.Id == id);
        if (artistToBeDeleted == null)
        {
            await Shell.Current.DisplayAlert("Error", "Artist not found", "OK");
            return artists;
        }

        if (artistToBeDeleted.IsDeleted)
        {
            await Shell.Current.DisplayAlert("Error", "Artist already deleted", "OK");
            return artists;
        }

        artistToBeDeleted.IsDeleted = true;
        var json = JsonSerializer.Serialize<ObservableCollection<Artist>>(artists);
        await File.WriteAllTextAsync(_artistFilePath, json);

        artists.Remove(artistToBeDeleted);
        await Shell.Current.DisplayAlert("Delete Artist", "Successfully deleted artist", "OK");
        return await GetActiveArtistAsync();
    }

    public async Task<ObservableCollection<Artist>> UpdateArtistAsync(int id)
    {
        var artists = await GetArtistsAsync();
        var artistToBeUpdated = artists.FirstOrDefault(m => m.Id == id);
        if (artistToBeUpdated == null)
        {
            await Shell.Current.DisplayAlert("Error", "Artist not found", "OK");
            return artists;
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
                        newValue = await Shell.Current.DisplayPromptAsync($"Edit Artist {selectedOption}", $"Enter new {selectedOption}:", initialValue: artistToBeUpdated.Email);
                        artistToBeUpdated.Email = newValue;
                        break;
                    case 1: // Username
                        newValue = await Shell.Current.DisplayPromptAsync($"Edit Artist {selectedOption}", $"Enter new {selectedOption}:", initialValue: artistToBeUpdated.Username);
                        artistToBeUpdated.Username = newValue;
                        break;
                    case 2: // Password
                        newValue = await Shell.Current.DisplayPromptAsync($"Edit Artist {selectedOption}", $"Enter new {selectedOption}:", initialValue: artistToBeUpdated.Password);
                        artistToBeUpdated.Password = newValue;
                        break;
                    case 3: // first name
                        newValue = await Shell.Current.DisplayPromptAsync($"Edit Artist {selectedOption}", $"Enter new {selectedOption}:", initialValue: artistToBeUpdated.FirstName);
                        artistToBeUpdated.FirstName = newValue;
                        break;
                    case 4: // last name
                        newValue = await Shell.Current.DisplayPromptAsync($"Edit Artist {selectedOption}", $"Enter new {selectedOption}:", initialValue: artistToBeUpdated.LastName);
                        artistToBeUpdated.LastName = newValue;
                        break;
                    default:
                        break;
                }
                break;
            }
        }

        int count = artists.ToList().FindIndex(m => m.Id == id);
        artists[count] = artistToBeUpdated;
        var json = JsonSerializer.Serialize<ObservableCollection<Artist>>(artists);
        await File.WriteAllTextAsync(_artistFilePath, json);

        await Shell.Current.DisplayAlert("Update Artist", "Successfully updated artist", "OK");
        return await GetActiveArtistAsync();
    }

    public async Task<ObservableCollection<Artist>> GetArtistsAsync()
    {
        if (!File.Exists(_artistFilePath))
        {
            return new ObservableCollection<Artist>();
        }

        var json = await File.ReadAllTextAsync(_artistFilePath);
        var artists = JsonSerializer.Deserialize<ObservableCollection<Artist>>(json);
        return artists!;
    }

    public async Task<ObservableCollection<Artist>> GetActiveArtistAsync()
    {
        var artists = await GetArtistsAsync();
        return new ObservableCollection<Artist>(artists.Where(m => !m.IsDeleted));
    }
}

using BeatSync.Models;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace BeatSync.Services;

public class AdminService
{
    private readonly string _artistFilePath = Path.Combine(FileSystem.Current.AppDataDirectory, "Artists.json");
    private readonly string _userFilePath = Path.Combine(FileSystem.Current.AppDataDirectory, "Users.json");
    private readonly string _publisherFilePath = Path.Combine(FileSystem.Current.AppDataDirectory, "Publishers.json");
    private readonly string _songFilePath = Path.Combine(FileSystem.Current.AppDataDirectory, "Songs.json");

    //logout
    public async Task Logout()
    {
        bool answer = await Shell.Current.DisplayAlert("Logout", "Would you like to log out?", "Yes", "No");
        if (answer)
        {
            Application.Current!.MainPage = new AppShell();
        }
    }

    //file and dirs
    public void CreateDirectoryIfMissing(string dir)
    {
        bool folderExists = Directory.Exists(dir);
        if (!folderExists)
            Directory.CreateDirectory(dir);
    }

    //Artists

    public async Task<bool> AddArtistAsync(Artist artist)
    {
        
        if (artist == null)
        {
            return false;
        }

        ObservableCollection<Artist> artists = await GetArtistsAsync();
        
        artist.Id = artists.Count+1;
        artist.AccounType = 1;
        artist.IsDeleted = false;

        artists.Add(artist);

        var json = JsonSerializer.Serialize<ObservableCollection<Artist>>(artists);
        await File.WriteAllTextAsync(_artistFilePath, json);
        return true;
        
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

        string[] editOptions = { "Email", "Username", "Password", "First Name", "Last Name"};
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

    public async Task<ObservableCollection<Artist>> GetActiveArtistAsync()
    {
        var artists = await GetArtistsAsync();
        return new ObservableCollection<Artist>(artists.Where(m => !m.IsDeleted));
    }


    //Songs

    public async Task<bool> AddSongAsync(Song song)
    {

        if (song == null)
        {
            return false;
        }

        ObservableCollection<Song> songs = await GetSongsAsync();

        song.Id = songs.Count + 1;
        
        songs.Add(song);

        var json = JsonSerializer.Serialize<ObservableCollection<Song>>(songs);
        await File.WriteAllTextAsync(_songFilePath, json);
        return true;
    }
    
    public async Task<FileResult?> UploadSongAsync()
    {
        var customFileType = new FilePickerFileType(
                new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.iOS, new[] { "public.my.comic.extension" } }, // UTType values
                    { DevicePlatform.Android, new[] { "audio/mpeg" } }, // MIME type
                    { DevicePlatform.WinUI, new[] { ".mp3" } }, // file extension
                    { DevicePlatform.Tizen, new[] { "*/*" } },
                    { DevicePlatform.macOS, new[] { "cbr", "cbz" } }, // UTType values
                });

        var fileResult = await FilePicker.PickAsync(new PickOptions
        {
            PickerTitle = "Please pick a song",
            FileTypes = customFileType
        });
        
        return fileResult;
    }

    public async Task<ObservableCollection<Song>> GetSongsAsync()
    {

        if (!File.Exists(_songFilePath))
        {
            return new ObservableCollection<Song>();
        }

        var json = await File.ReadAllTextAsync(_songFilePath);
        var songs = JsonSerializer.Deserialize<ObservableCollection<Song>>(json);
        return songs!;
    }

    public async Task<ObservableCollection<Song>> GetActiveSongAsync()
    {
        var songs = await GetSongsAsync();
        return new ObservableCollection<Song>(songs.Where(m => !m.IsDeleted));
    }

    public async Task<String> GetArtistNameById(int id )
    {
        var artists = await GetArtistsAsync();
        var artist = artists.FirstOrDefault(m => m.Id == id);
        return $"{artist!.FirstName} {artist!.LastName}";
    }

    public async Task<ObservableCollection<Song>> DeleteSongAsync(int id)
    {
        var songs = await GetSongsAsync();
        var songToBeDeleted = songs.FirstOrDefault(m => m.Id == id);
        if (songToBeDeleted == null)
        {
            await Shell.Current.DisplayAlert("Error", "Song not found", "OK");
            return songs;
        }

        if (songToBeDeleted.IsDeleted)
        {
            await Shell.Current.DisplayAlert("Error", "Song already deleted", "OK");
            return songs;
        }

        songToBeDeleted.IsDeleted = true;
        var json = JsonSerializer.Serialize<ObservableCollection<Song>>(songs);
        await File.WriteAllTextAsync(_songFilePath, json);

        songs.Remove(songToBeDeleted);
        await Shell.Current.DisplayAlert("Delete Song", "Successfully deleted song", "OK");
        return await GetActiveSongAsync();
    }

    public async Task<ObservableCollection<Song>> UpdateSongAsync(int id)
    {
        var songs = await GetSongsAsync();
        var songToBeUpdated = songs.FirstOrDefault(m => m.Id == id);
        if (songToBeUpdated == null)
        {
            await Shell.Current.DisplayAlert("Error", "Song not found", "OK");
            return songs;
        }

        string[] editOptions = { "Name" };
        string selectedOption = await Shell.Current.DisplayActionSheet("Select Property to Edit", "Cancel", null, editOptions);

        var newValue = string.Empty;
        for (int index = 0; index < editOptions.Length; index++)
        {
            if (editOptions[index] == selectedOption)
            {
                switch (index)
                {
                    case 0: // Email
                        newValue = await Shell.Current.DisplayPromptAsync($"Edit Artist {selectedOption}", $"Enter new {selectedOption}:", initialValue: songToBeUpdated.Name);
                        songToBeUpdated.Name = newValue;
                        break;
                    default:
                        break;
                }
                break;
            }
        }

        int count = songs.ToList().FindIndex(m => m.Id == id);
        songs[count] = songToBeUpdated;
        var json = JsonSerializer.Serialize<ObservableCollection<Song>>(songs);
        await File.WriteAllTextAsync(_songFilePath, json);

        await Shell.Current.DisplayAlert("Update song", "Successfully updated song", "OK");
        return await GetActiveSongAsync();
    }

   
    //Publishers
    public async Task<bool> AddPublisherAsync(Publisher publisher)
    {
        
        if (publisher == null)
        {
            return false;
        }

        ObservableCollection<Publisher> publishers = await GetPublishersAsync();
        
        publisher.Id = publishers.Count+1;
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

        string[] editOptions = { "Email", "Username", "Password", "First Name", "Last Name"};
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

    public async Task<ObservableCollection<Publisher>> GetActivePublisherAsync()
    {
        var publishers = await GetPublishersAsync();
        return new ObservableCollection<Publisher>(publishers.Where(m => !m.IsDeleted));
    }

    //Users

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
        await File.WriteAllTextAsync(_userFilePath, json);
        return true;

    }

    public async Task<ObservableCollection<User>> GetUsersAsync()
    {

        if (!File.Exists(_userFilePath))
        {
            return new ObservableCollection<User>();
        }

        var json = await File.ReadAllTextAsync(_userFilePath);
        var users = JsonSerializer.Deserialize<ObservableCollection<User>>(json);
        return users!;
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
        await File.WriteAllTextAsync(_userFilePath, json);

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
        await File.WriteAllTextAsync(_userFilePath, json);

        await Shell.Current.DisplayAlert("Update User", "Successfully updated user", "OK");
        return await GetActiveUserAsync();
    }

    public async Task<ObservableCollection<User>> GetActiveUserAsync()
    {
        var users = await GetUsersAsync();
        return new ObservableCollection<User>(users.Where(m => !m.IsDeleted));
    }

}

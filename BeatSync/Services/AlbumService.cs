namespace BeatSync.Services;

public class AlbumService
{
    private readonly string albumFilePath = Path.Combine(FileSystem.Current.AppDataDirectory, "Albums.json");
    public async Task<bool> AddAlbumAsync(Album album)
    {
        if (album == null)
        {
            return false;
        }

        ObservableCollection<Album> albums = await GetAlbumsAsync();

        album.Id = albums.Count + 1;
        album.IsDeleted = false;

        albums.Add(album);

        var json = JsonSerializer.Serialize<ObservableCollection<Album>>(albums);
        await File.WriteAllTextAsync(albumFilePath, json);
        return true;
    }

    public async Task<ObservableCollection<Album>> UpdateAlbumAsync(string? albumName)
    {
        var albums = await GetAlbumsAsync();
        var albumToBeUpdated = albums.FirstOrDefault(a => a.Name == albumName);
        if (albumToBeUpdated == null)
        {
            await Shell.Current.DisplayAlert("Error", "Album not found", "OK");
            return albums;
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
                        newValue = await Shell.Current.DisplayPromptAsync($"Edit Album {selectedOption}", $"Enter new {selectedOption}:", initialValue: albumToBeUpdated.Name);
                        albumToBeUpdated.Name = newValue;
                        break;
                    default:
                        break;
                }
                break;
            }
        }

        int count = albums.ToList().FindIndex(a => a.Name == albumName);
        albums[count] = albumToBeUpdated;
        var json = JsonSerializer.Serialize<ObservableCollection<Album>>(albums);
        await File.WriteAllTextAsync(albumFilePath, json);

        await Shell.Current.DisplayAlert("Update album", "Successfully updated album", "OK");
        return await GetActiveAlbumsAsync();
    }

    public async Task<ObservableCollection<Album>> DeleteAlbumAsync(string? albumName)
    {
        var albums = await GetAlbumsAsync();
        var albumToBeDeleted = albums.FirstOrDefault(a => a.Name == albumName);
        if (albumToBeDeleted == null)
        {
            await Shell.Current.DisplayAlert("Error", "Album not found", "OK");
            return albums;
        }

        if (albumToBeDeleted.IsDeleted)
        {
            await Shell.Current.DisplayAlert("Error", "Album already deleted", "OK");
            return albums;
        }

        albumToBeDeleted.IsDeleted = true;
        var json = JsonSerializer.Serialize<ObservableCollection<Album>>(albums);
        await File.WriteAllTextAsync(albumFilePath, json);

        albums.Remove(albumToBeDeleted);
        await Shell.Current.DisplayAlert("Delete Song", "Successfully deleted song", "OK");
        return await GetActiveAlbumsAsync();
    }

    public async Task<ObservableCollection<Album>> GetAlbumsAsync()
    {
        if (!File.Exists(albumFilePath))
        {
            return new ObservableCollection<Album>();
        }

        var json = await File.ReadAllTextAsync(albumFilePath);
        var albums = JsonSerializer.Deserialize<ObservableCollection<Album>>(json);
        return albums!;
    }

    public async Task<ObservableCollection<Album>> GetActiveAlbumsAsync()
    {
        var albums = await GetAlbumsAsync();
        return new ObservableCollection<Album>(albums.Where(a => !a.IsDeleted));
    }

    public async Task<ObservableCollection<Song>> AddAlbumSongAsync(Album album)
    {
        var albums = await GetAlbumsAsync();

        var indexOfAlbumInTheCollection = albums.ToList().FindIndex(a => a.Id == album.Id);
        albums[indexOfAlbumInTheCollection] = album;

        var json = JsonSerializer.Serialize<ObservableCollection<Album>>(albums);
        await File.WriteAllTextAsync(albumFilePath, json);

        await Shell.Current.DisplayAlert("Add Album song", "Successfully added song to album", "OK");
        return album.Songs!;
    }
}

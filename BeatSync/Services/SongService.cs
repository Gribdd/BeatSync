using BeatSync.Models;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace BeatSync.Services;

public class SongService
{
    private readonly string songFilePath = Path.Combine(FileSystem.Current.AppDataDirectory, "Songs.json");
    public async Task<bool> AddSongAsync(Song song)
    {

        if (song == null)
        {
            return false;
        }

        ObservableCollection<Song> songs = await GetSongsAsync();

        song.AlbumId = null;
        song.Id = songs.Count + 1;

        songs.Add(song);

        var json = JsonSerializer.Serialize<ObservableCollection<Song>>(songs);
        await File.WriteAllTextAsync(songFilePath, json);
        return true;
    }

    public async Task<ObservableCollection<Song>> GetSongsAsync()
    {

        if (!File.Exists(songFilePath))
        {
            return new ObservableCollection<Song>();
        }

        var json = await File.ReadAllTextAsync(songFilePath);
        var songs = JsonSerializer.Deserialize<ObservableCollection<Song>>(json);
        return songs!;
    }

    public async Task<ObservableCollection<Song>> GetActiveSongAsync()
    {
        var songs = await GetSongsAsync();
        return new ObservableCollection<Song>(songs.Where(m => !m.IsDeleted));
    }

    public async Task<ObservableCollection<Song>> GetSongsByArtistIdAsync(int? artistId)
    {
        if (artistId == null) return new ObservableCollection<Song>();

        var songs = await GetSongsAsync();

        return new ObservableCollection<Song>(songs.Where(song => song.ArtistID == artistId));
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
        await File.WriteAllTextAsync(songFilePath, json);

        songs.Remove(songToBeDeleted);
        await Shell.Current.DisplayAlert("Delete Song", "Successfully deleted song", "OK");
        return await GetActiveSongAsync();
    }

    public async Task<ObservableCollection<Song>> UpdateSongAsync(int? id)
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
        await File.WriteAllTextAsync(songFilePath, json);

        await Shell.Current.DisplayAlert("Update song", "Successfully updated song", "OK");
        return await GetActiveSongAsync();
    }

    //method overloading for updating album id of song 
    public async Task UpdateSongAsync(Song song)
    {
        var songs = await GetSongsAsync();

        var indexOfSongInTheCollection = songs.ToList().FindIndex(s => s.Id == song.Id);
        songs[indexOfSongInTheCollection] = song;

        var json = JsonSerializer.Serialize<ObservableCollection<Song>>(songs);
        await File.WriteAllTextAsync(songFilePath, json);
    }

}

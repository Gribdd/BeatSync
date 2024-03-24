using BeatSync.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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

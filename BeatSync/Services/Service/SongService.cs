using BeatSync.Services.IService;

namespace BeatSync.Services.Service;

public class SongService : GenericService<Song>, ISongService
{
    private IUnitofWork _unitofWork;
    public SongService(IUnitofWork unitofWork) : base(unitofWork)
    {
        _unitofWork = unitofWork;
    }

    public async Task<ObservableCollection<Song>> GetSongsByArtistIdAsync(int? artistId)
    {
        if (artistId == null) return new ObservableCollection<Song>();

        var songs = await GetAllAsync();

        return new ObservableCollection<Song>(songs.Where(song => song.ArtistID == artistId));
    }

    public override async Task UpdateAsync(int id)
    {
        var songToBeUpdated = await GetAsync(id);

        string[] editOptions = { "Name" };
        string selectedOption = await Shell.Current.DisplayActionSheet("Select Property to Edit", "Cancel", null, editOptions);

        var newValue = string.Empty;
        for (int index = 0; index < editOptions.Length; index++)
        {
            if (editOptions[index] == selectedOption)
            {
                switch (index)
                {
                    case 0: //Name
                        newValue = await Shell.Current.DisplayPromptAsync($"Edit Artist {selectedOption}", $"Enter new {selectedOption}:", initialValue: songToBeUpdated.Name);
                        songToBeUpdated.Name = newValue;
                        break;
                    default:
                        break;
                }
                break;
            }
        }
        await base.UpdateAsync(songToBeUpdated);
    }

    //method overloading for updating album id of song 
    public override async Task UpdateAsync(Song song)
    {
        var songToBeUpdated = await GetAsync(song.Id);
        await base.UpdateAsync(songToBeUpdated);
    }

    public async Task<ObservableCollection<Song>> GetSongsBySearchQuery(string? query)
    {
        if (string.IsNullOrEmpty(query))
        {
            return new ObservableCollection<Song>();
        }

        var songs = await GetActiveAsync();
        return new ObservableCollection<Song>(songs.Where(s => s.Name!.Contains(query)));
    }

    public async Task<Song> GetSongBySongId(int songId, ObservableCollection<Song> songs)
    {
        var song = new Song();
        if (songId <= 0)
        {
            return song;
        }

        return songs.FirstOrDefault(s => s.Id == songId)!;
    }
    public async Task<ObservableCollection<Song>> GetSongsBySongIds(List<int> songIds)
    {
        var songs = new ObservableCollection<Song>();
        if (songIds == null)
        {
            return songs;
        }

        var allSongs = await GetAllAsync();
        foreach (var songId in songIds)
        {
            var song = await GetSongBySongId(songId, allSongs);
            if (song != null)
            {
                songs.Add(song);
            }
        }

        return songs;
    }

    public async Task<Song> GetByNameAsync(string name)
    {
        return await _unitofWork.SongRepository.GetSongByName(name);
    }
}

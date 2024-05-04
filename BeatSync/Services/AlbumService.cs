namespace BeatSync.Services;

public class AlbumService : GenericService<Album>, IAlbumService
{
    private readonly IUnitofWork _unitofWork;
    public AlbumService(IUnitofWork unitofWork) : base(unitofWork)
    {
        _unitofWork = unitofWork;
    }

    public override async Task UpdateAsync(int id)
    {
        var albumToBeUpdated = await GetAsync(id);
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
                        newValue = await Shell.Current.DisplayPromptAsync($"Edit Album {selectedOption}", $"Enter new {selectedOption}:", initialValue: albumToBeUpdated.Name);
                        albumToBeUpdated.Name = newValue;
                        break;
                    default:
                        break;
                }
                break;
            }
        }

        await base.UpdateAsync(albumToBeUpdated);
    }

    //we are just updating the album songs
    //used for adding and removing songs from an album
    public async Task UpdateAlbumSongs(Album album)
    {
        await base.UpdateAsync(album);
    }

    public Task<Album> GetByNameAsync(string albumName)
    {
        return _unitofWork.AlbumRepository.GetByName(albumName);
    }

    public Task<Album> GetByNameAndArtistIdAsync(string albumName, int artistId)
    {
        return _unitofWork.AlbumRepository.GetByNameAndArtistId(albumName, artistId);
    }

    public Task<ObservableCollection<Album>> GetByArtistId(int artistId)
    {
        return _unitofWork.AlbumRepository.GetByArtistId(artistId);
    }


}

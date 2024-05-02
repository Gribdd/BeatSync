namespace BeatSync.Selector;
/// <summary>
/// checks if the item is an Artist, Album, or Song and returns the appropriate DataTemplate.
/// </summary>
public class SearchBarDataTemplateSelector : DataTemplateSelector
{
    public DataTemplate? ArtistTemplate { get; set; }
    public DataTemplate? PlaylistTemplate { get; set; } 
    public DataTemplate? AlbumTemplate { get; set; }
    public DataTemplate? SongTemplate { get; set; }
    public DataTemplate? PublisherTemplate { get; set;  }
    public DataTemplate? UserTemplate { get; set; }

    protected override DataTemplate? OnSelectTemplate(object item, BindableObject container)
    {
        switch (item)
        {
            case Artist _:
                return ArtistTemplate;
            case Song _:
                return SongTemplate;
            case Album _:
                return AlbumTemplate;
            case Playlist _:
                return PlaylistTemplate;
            case Publisher _:
                return PublisherTemplate;
            case User _:
                return UserTemplate;
            default:
                return null;
        }
    }
}

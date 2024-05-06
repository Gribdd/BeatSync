namespace BeatSync.Selector;
public class LandingPageSelector : DataTemplateSelector
{
    public DataTemplate? UserTemplate { get; set; }
    public DataTemplate? PublisherTemplate { get; set; }
    public DataTemplate? ArtistTemplate { get; set; }

    protected override DataTemplate? OnSelectTemplate(object item, BindableObject container)
    {
        switch (item)
        {
            case User _:
                return UserTemplate;
            case Publisher _:
                return PublisherTemplate;
            case Artist _:
                return ArtistTemplate;
            default:
                return null;
        }
    }
}

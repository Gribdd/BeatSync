namespace BeatSync.Selector;

public class HeartIconTemplateSelector : DataTemplateSelector
{
    public DataTemplate FavoriteTemplate { get; set; }
    public DataTemplate NotFavoriteTemplate { get; set; }

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        var song = (Song)item;
        var viewModel = (AddPlaylistSongsCustomerViewModel)container.BindingContext;
        return viewModel.IsFavorite(song) ? FavoriteTemplate : NotFavoriteTemplate;
    }
}

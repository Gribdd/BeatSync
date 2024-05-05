using Microsoft.Maui.Controls;

namespace BeatSync.Selector
{
    public class LandingPageSelector : DataTemplateSelector
    {
        public DataTemplate? UserTemplate { get; set; }
        public DataTemplate? PublisherTemplate { get; set; }
        public DataTemplate? ArtistTemplate { get; set; }

        protected override DataTemplate? OnSelectTemplate(object item, BindableObject container)
        {
            return item switch
            {
                User _ => UserTemplate,
                Publisher _ => PublisherTemplate,
                Artist _ => ArtistTemplate,
                _ => null, // Return null if no matching template found
            };
        }
    }
}

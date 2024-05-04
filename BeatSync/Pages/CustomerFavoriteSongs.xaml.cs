namespace BeatSync.Pages;

public partial class CustomerFavoriteSongs : ContentPage
{
	public CustomerFavoriteSongs(CustomerFavoriteSongsViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}
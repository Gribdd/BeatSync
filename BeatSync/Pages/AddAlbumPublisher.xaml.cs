namespace BeatSync.Pages;

public partial class AddAlbumPublisher : ContentPage
{
    AddAlbumPublisherViewModel _vm;
    public AddAlbumPublisher(AddAlbumPublisherViewModel vm)
    {
        BindingContext = _vm = vm;
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _vm.GetArtists();
    }
}
using BeatSync.ViewModel.PublisherShell;

namespace BeatSync.Pages;

public partial class AddAlbumPublisher : ContentPage
{
    AddAlbumPublisherViewModel _vm;
    public AddAlbumPublisher(AddAlbumPublisherViewModel vm)
    {
        BindingContext = _vm = vm;
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        _vm.GetArtists();

        if (_vm.Artists.Count == 0)
        {
            await Shell.Current.DisplayAlert(
              "No Artists Available",
              "There are currently no artists in your library. You cannot create an album without an artist.",
              "OK"
            );

            await Shell.Current.GoToAsync("..");
        }
    }

}
namespace BeatSync.Pages;

public partial class ArtistManagement : ContentPage
{
    ArtistManagementViewModel _vm;

    public ArtistManagement(ArtistManagementViewModel vm)
	{
		InitializeComponent();
        BindingContext = _vm = vm;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _vm.GetArtists();
    }
}
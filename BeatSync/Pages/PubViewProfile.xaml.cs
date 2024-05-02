namespace BeatSync.Pages;

public partial class PubViewProfile : ContentPage
{
	PublisherLandingPageViewModel _vm;
	public PubViewProfile(PublisherLandingPageViewModel vm)
	{
		BindingContext = _vm = vm;
        InitializeComponent();
	}

	protected override void OnAppearing()
    {
        base.OnAppearing();
        _vm.GetActivePublisher();
        _vm.GetAlbums();
    }
}
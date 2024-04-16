namespace BeatSync.Views;

public partial class CustomerLandingPage : Shell
{
    CustomerLandingPageViewModel _vm;
    public CustomerLandingPage(CustomerLandingPageViewModel vm)
    {
        _vm = vm;
        BindingContext = _vm;
        InitializeComponent();

        RegisterRoutes();
    }

    void RegisterRoutes()
    {
        Routing.RegisterRoute($"library/{nameof(AddPlaylistCustomer)}", typeof(AddPlaylistCustomer));
        Routing.RegisterRoute($"library/{nameof(AddPlaylistSongsCustomer)}", typeof(AddPlaylistSongsCustomer));
        Routing.RegisterRoute($"library/{nameof(AddPlaylistSongsCustomer)}/{nameof(AddPlaylistSongsSearch)}", typeof(AddPlaylistSongsSearch));
        Routing.RegisterRoute(nameof(ViewProfile), typeof(ViewProfile));
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await _vm.GetActiveCustomer();
    }
}


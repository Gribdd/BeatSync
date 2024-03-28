namespace BeatSync.Pages;

public partial class PublisherManagement : ContentPage
{
    PublisherManagementViewModel _vm;

    public PublisherManagement(PublisherManagementViewModel vm)
    {
        InitializeComponent();
        BindingContext = _vm = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _vm.GetPublishers();
    }
}
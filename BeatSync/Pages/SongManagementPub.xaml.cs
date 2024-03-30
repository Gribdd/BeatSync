namespace BeatSync.Pages;

public partial class SongManagementPub : ContentPage
{
    SongManagementPubViewModel _vm;
    public SongManagementPub(SongManagementPubViewModel vm)
    {
        BindingContext = _vm = vm;
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _vm.GetSongsAsync();
        _vm.GetActivePublisher();
    }

}
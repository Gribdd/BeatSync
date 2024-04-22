namespace BeatSync.Pages;

public partial class PubRecentlyPlayed : ContentPage
{
    PubRecentlyPlayedViewModel _vm;
    public PubRecentlyPlayed(PubRecentlyPlayedViewModel vm)
    {
        InitializeComponent();
        BindingContext = _vm = vm;

    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _vm.LoadRecentlyPlayed();
    }
}
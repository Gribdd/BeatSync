
using BeatSync.ViewModel.LoginAndRegistration;

namespace BeatSync;

public partial class MainPage : ContentPage
{
    public MainPage(MainPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}

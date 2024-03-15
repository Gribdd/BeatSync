
using BeatSync.ViewModel.PublisherShell;

namespace BeatSync.Pages;

public partial class SongManagementPub : ContentPage
{
    public SongManagementPub(SongManagementPubViewModel vm)
    {
        BindingContext = vm;
        InitializeComponent();
    }

}
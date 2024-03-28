namespace BeatSync.Pages;

public partial class SongManagement : ContentPage
{
	SongManagementViewModel _vm;
	public SongManagement(SongManagementViewModel vm)
	{
		InitializeComponent();
		BindingContext = _vm = vm;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
		_vm.GetSongs();
    }

}
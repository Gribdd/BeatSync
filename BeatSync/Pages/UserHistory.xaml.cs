namespace BeatSync.Pages;

public partial class UserHistory : ContentPage
{
	UserHistoryViewModel _vm;
	public UserHistory(UserHistoryViewModel vm)
	{
		InitializeComponent();
		BindingContext = _vm = vm;
	}

	protected override void OnAppearing()
	{
        base.OnAppearing();
    }
}
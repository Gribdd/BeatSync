using BeatSync.ViewModel.Users;

namespace BeatSync.Pages;

public partial class SearchPage : ContentPage
{
	SearchPageViewModel _vm;
	public SearchPage(SearchPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = _vm = vm;
	}

	protected override void OnAppearing()
	{
        base.OnAppearing();
    }
}
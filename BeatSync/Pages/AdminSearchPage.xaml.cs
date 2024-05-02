namespace BeatSync.Pages;

public partial class AdminSearchPage : ContentPage
{
	AdminSearchPageViewModel _vm;
	public AdminSearchPage(AdminSearchPageViewModel vm)
	{
		InitializeComponent();
        BindingContext = _vm = vm;	
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }

    private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(e.NewTextValue))
        {
            // Clear the search results
            _vm.SearchQuery = string.Empty;
            _vm.MyList.Clear();
            ((AdminSearchPageViewModel)BindingContext).IsResultsVisible = false;

        }
    }
}
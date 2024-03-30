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
        _vm.LoadCurrentUser();
    }

    private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(e.NewTextValue))
        {
            // Clear the search results
            _vm.SearchQuery = string.Empty;
            _vm.MyList.Clear();
            ((SearchPageViewModel)BindingContext).IsResultsVisible = false;

        }
    }
}
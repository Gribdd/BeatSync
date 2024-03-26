using BeatSync.ViewModel.Users;
using BeatSync.Services;
using System.IO;

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

    private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(e.NewTextValue))
        {
            // Clear the search results
            _vm.SearchQuery = string.Empty;
            _vm.MyList.Clear();
        }
        
    }
}
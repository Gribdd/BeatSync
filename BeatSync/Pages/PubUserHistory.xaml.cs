using BeatSync.ViewModel.PublisherShell;

namespace BeatSync.Pages;

public partial class PubUserHistory : ContentPage
{
    PubUserHistoryViewModel _viewModel;

	public PubUserHistory(PubUserHistoryViewModel vm)
    {
        InitializeComponent();
        BindingContext = _viewModel = vm;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.GetUserHistories();
        _viewModel.GetActivePublisher();
    }
}
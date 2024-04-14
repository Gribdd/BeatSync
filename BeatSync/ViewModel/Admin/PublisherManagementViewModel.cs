
namespace BeatSync.ViewModel.Admin
{
    public partial class PublisherManagementViewModel : ObservableObject
    {
        private readonly PublisherService _publisherService;

        [ObservableProperty]
        private ObservableCollection<Publisher> _publishers = new();

        public PublisherManagementViewModel( PublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        [RelayCommand]
        async Task NavigateAddPublisher()
        {
            await Shell.Current.GoToAsync($"{nameof(AddPublisher)}");
        }

        [RelayCommand]
        async Task DeletePublisher()
        {
            string inputId = await Shell.Current.DisplayPromptAsync("Delete Publisher", "Enter Publisher ID to delete:");
            if (!string.IsNullOrEmpty(inputId) && int.TryParse(inputId, out int id))
            {
                await _publisherService.DeleteAsync(id);
            }
            Publishers = await _publisherService.GetActiveAsync();
        }

        [RelayCommand]
        async Task UpdatePublisher()
        {
            string inputId = await Shell.Current.DisplayPromptAsync("Update Publisher", "Enter Publisher ID to update:");
            if (!string.IsNullOrEmpty(inputId) && int.TryParse(inputId, out int id))
            {
                await _publisherService.UpdateAsync(id);
            }
            Publishers = await _publisherService.GetActiveAsync();
        }

        [RelayCommand]
        async Task Logout()
        {
        }

        public async void GetPublishers()
        {
            Publishers = await _publisherService.GetActiveAsync();
        }
    }
}


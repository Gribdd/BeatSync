
namespace BeatSync.ViewModel.Admin
{
    public partial class PublisherManagementViewModel : ObservableObject
    {
        private PublisherService publisherService;

        [ObservableProperty]
        private ObservableCollection<Publisher> _publishers = new();

        public PublisherManagementViewModel( PublisherService publisherService)
        {
            this.publisherService = publisherService;
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
                Publishers = await publisherService.DeletePublisherAsync(id);
            }
        }

        [RelayCommand]
        async Task UpdatePublisher()
        {
            string inputId = await Shell.Current.DisplayPromptAsync("Update Publisher", "Enter Publisher ID to delete:");
            if (!string.IsNullOrEmpty(inputId) && int.TryParse(inputId, out int id))
            {
                Publishers = await publisherService.UpdatePublisherAsync(id);
            }
        }

        [RelayCommand]
        async Task Logout()
        {
        }

        public async void GetPublishers()
        {
            Publishers = await publisherService.GetActivePublisherAsync();
        }
    }
}


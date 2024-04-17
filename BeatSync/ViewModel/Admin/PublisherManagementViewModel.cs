
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
            string username = await Shell.Current.DisplayPromptAsync("Delete Artist", "Enter Publisher username to delete:");
            if (!string.IsNullOrEmpty(username))
            {
                var publisher = await _publisherService.GetByUsernameAsync(username);
                await _publisherService.DeleteAsync(publisher.Id);
            }
            Publishers = await _publisherService.GetActiveAsync();
        }

        [RelayCommand]
        async Task UpdatePublisher()
        {
            string username = await Shell.Current.DisplayPromptAsync("Update Artist", "Enter Publisher username to update:");
            if (!string.IsNullOrEmpty(username))
            {
                var publisher = await _publisherService.GetByUsernameAsync(username);
                await _publisherService.UpdateAsync(publisher.Id);
            }
            Publishers = await _publisherService.GetActiveAsync();
        }

        [RelayCommand]
        async Task Logout()
        {
            bool answer = await Shell.Current.DisplayAlert("Logout", "Would you like to log out?", "Yes", "No");
            if (answer)
            {
                Application.Current!.MainPage = new AppShell();
                Preferences.Default.Set("currentUserId", -1);
            }
        }

        public async void GetPublishers()
        {
            Publishers = await _publisherService.GetActiveAsync();
        }
    }
}


using BeatSync.Models;
using BeatSync.Pages;
using BeatSync.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;


namespace BeatSync.ViewModel.Admin
{
    public partial class PublisherManagementViewModel : ObservableObject
    {
        private AdminService _adminService;

        [ObservableProperty]
        private ObservableCollection<Publisher> _publishers = new();

        public PublisherManagementViewModel(AdminService adminService)
        {
            _adminService = adminService;
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
                Publishers = await _adminService.DeletePublisherAsync(id);
            }
        }

        [RelayCommand]
        async Task UpdatePublisher()
        {
            string inputId = await Shell.Current.DisplayPromptAsync("Update Publisher", "Enter Publisher ID to delete:");
            if (!string.IsNullOrEmpty(inputId) && int.TryParse(inputId, out int id))
            {
                Publishers = await _adminService.UpdatePublisherAsync(id);
            }
        }

        [RelayCommand]
        async Task Logout()
        {
            await _adminService.Logout();
        }

        public ICommand GetPublishersCommand => new Command(GetPublishers);
        public async void GetPublishers()
        {
            Publishers = await _adminService.GetActivePublisherAsync();
        }
    }
}


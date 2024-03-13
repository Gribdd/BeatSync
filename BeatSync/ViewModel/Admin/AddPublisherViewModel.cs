using BeatSync.Models;
using BeatSync.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;


namespace BeatSync.ViewModel.Admin
{
    public partial class AddPublisherViewModel : ObservableObject
    {
        private AdminService _adminService;

        [ObservableProperty]
        private Publisher _publisher = new();

        public AddPublisherViewModel(AdminService adminService)
        {
            _adminService = adminService;
        }

        [RelayCommand]
        async Task AddPublisher()
        {
            if (await _adminService.AddPublisherAsync(Publisher))
            {
                await Shell.Current.DisplayAlert("Add Publisher", "Publisher successfully added", "OK");
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                await Shell.Current.DisplayAlert("Add Publisher", "Please enter all fields", "OK");
            }
        }

        [RelayCommand]
        async Task Return()
        {
            await Shell.Current.GoToAsync("..");
        }

    }
}


using BeatSync.Services.Service;

namespace BeatSync.ViewModel.General
{
    public partial class ViewProfileViewModel : ObservableObject
    {

        private User _user;
        public User User
        {
            get { return _user; }
            set { SetProperty(ref _user, value); }
        }


        [ObservableProperty]
        UserService _userService;

        public ViewProfileViewModel(UserService userService)
        {
            UserService = userService;
        }

        public async void GetUser()
        {
            await UserService.GetCurrentUser();
        }

        [RelayCommand]
        async Task Return()
        {
            await Shell.Current.GoToAsync("..");
        }

    }
}

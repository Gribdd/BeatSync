
namespace BeatSync.ViewModel.LoginAndRegistration;

[QueryProperty(nameof(User), nameof(User))]
public partial class CreateAccountDOBViewModel : ObservableObject
{
    [ObservableProperty]
    private User _user = new();

    [RelayCommand]
    async Task Return()
    {
        await Shell.Current.GoToAsync("..");
    }


    [RelayCommand]
    async Task NavigateToCreateFirstName()
    {
        if (User.DateOfBirth >= DateTime.Now.Date)
        {
            await Shell.Current.DisplayAlert("Error!", "You cannot set your date of birth to today's date.", "Ok");
            return;
        }

        var navigationParameter = new Dictionary<string, object>
        {
            {nameof(User), User }
        };
        await Shell.Current.GoToAsync("createaccountfirstname", navigationParameter);
    }
}


namespace BeatSync.ViewModel.LoginAndRegistration;

[QueryProperty(nameof(User), nameof(User))]
public partial class CreateAccountFirstNameViewModel : ObservableObject
{
    [ObservableProperty]
    private User _user = new();

    [RelayCommand]
    async Task Return()
    {
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    async Task NavigateToCreateLastName()
    {
        if (string.IsNullOrWhiteSpace(User.FirstName))
        {
            await Shell.Current.DisplayAlert("Oops!", "You must enter a first name.", "Ok");
            return;
        }

        var navigationParameter = new Dictionary<string, object>
        {
            {nameof(User), User }
        };

        await Shell.Current.GoToAsync("createaccountlastname", navigationParameter);
    }
}

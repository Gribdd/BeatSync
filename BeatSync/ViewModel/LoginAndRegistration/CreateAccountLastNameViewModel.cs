namespace BeatSync.ViewModel.LoginAndRegistration;

[QueryProperty(nameof(User), nameof(User))]
public partial class CreateAccountLastNameViewModel : ObservableObject
{
    [ObservableProperty]
    private User _user = new();

    [RelayCommand]
    async Task Return()
    {
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    async Task NavigateToCreateGender()
    {
        if (string.IsNullOrWhiteSpace(User.LastName))
        {
            await Shell.Current.DisplayAlert("Oops!", "You must enter a last name.", "Ok");
            return;
        }

        var navigationParameter = new Dictionary<string, object>
        {
            {nameof(User), User }
        };
        await Shell.Current.GoToAsync("createaccountgender", navigationParameter);
    }
}

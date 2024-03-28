namespace BeatSync.Services;

public class AdminService
{
    /// <summary>
    /// Logouts user and sets Preferences currentUserId to -1 or null
    /// </summary>
    /// <returns>void</returns>
    public async Task Logout()
    {
        bool answer = await Shell.Current.DisplayAlert("Logout", "Would you like to log out?", "Yes", "No");
        if (answer)
        {
            Application.Current!.MainPage = new AppShell();
            Preferences.Default.Set("currentUserId", -1);
        }
    }
}

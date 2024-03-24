using BeatSync.Models;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace BeatSync.Services;

public class AdminService
{
    //logout
    public async Task Logout()
    {
        bool answer = await Shell.Current.DisplayAlert("Logout", "Would you like to log out?", "Yes", "No");
        if (answer)
        {
            Application.Current!.MainPage = new AppShell();
        }
    }
}

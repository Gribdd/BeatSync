using BeatSync.Services.Service;

namespace BeatSync.ViewModel.ArtistShell;

public partial class ArtistLandingPageViewModel : ObservableObject
{
	private readonly ArtistService artistService;
	private Artist _artist = new();


	public Artist Artist
	{
		get { return _artist; }
		set { SetProperty(ref _artist, value); }
	}

	public ArtistLandingPageViewModel (ArtistService artistService)
	{
        this.artistService = artistService;
    }

	[RelayCommand]
	async Task OnProfileIconClicked()
	{
        bool answer = await Shell.Current.DisplayAlert("Logout", "Would you like to log out?", "Yes", "No");
        if (answer)
		{
            Application.Current.MainPage = new AppShell();
            Preferences.Default.Set("currentUserId", -1);
        }
    }

	public async Task GetActiveArtist()
	{
        Artist = await artistService.GetCurrentUser();
    }

	[RelayCommand]
	async Task ViewProfile()
	{
		await GetActiveArtist();
		//await Shell.Current.Navigation.PushAsync(new ViewProfile(this));
	}

	[RelayCommand]
	async Task Return()
	{
        // Navigate back
        await Shell.Current.GoToAsync("..");
    }	
}
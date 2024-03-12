namespace BeatSync.Pages;

public partial class ArtistManagement : ContentPage
{
	public ArtistManagement()
	{
		InitializeComponent();
	}

    private async void OnBtnAddClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddArtist());
    }

    private async void OnBtnDeleteClicked(object sender, EventArgs e)
    {
        string inputId = await DisplayPromptAsync("Delete Artist", "Enter Artist ID to delete:");
    }
    private async void OnBtnUpdateClicked(object sender, EventArgs e)
    {
        string inputID = await DisplayPromptAsync("Edit Artist Details", "Enter Artist ID to Edit:");
    }
}
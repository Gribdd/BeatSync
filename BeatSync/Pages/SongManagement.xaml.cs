namespace BeatSync.Pages;

public partial class SongManagement : ContentPage
{
	public SongManagement()
	{
		InitializeComponent();
	}

	private async void OnBtnAddClicked(object sender, EventArgs e)
	{
        await Navigation.PushAsync(new AddSong());
    }

    private async void OnBtnDeleteClicked(object sender, EventArgs e)
	{
        string inputId = await DisplayPromptAsync("Delete Song", "Enter Song ID to delete:");
    }
    private async void OnBtnUpdateClicked(object sender, EventArgs e)
    {
        string inputID = await DisplayPromptAsync("Edit Song Details", "Enter Song ID to Edit:");
    }
}
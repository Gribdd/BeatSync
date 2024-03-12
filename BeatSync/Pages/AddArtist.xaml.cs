namespace BeatSync.Pages;

public partial class AddArtist : ContentPage
{
	public AddArtist()
	{
		InitializeComponent();
	}

    private void OnBtnReturnClicked(object sender, EventArgs e)
    {
		Navigation.PopAsync();
    }

    private void OnBtnAddClicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }


}
namespace BeatSync.Pages;

public partial class UserManagement : ContentPage
{
	UserManagementViewModel _vm;
	public UserManagement(UserManagementViewModel vm)
	{
		InitializeComponent();
        BindingContext = _vm = vm;
    }

	protected override void OnAppearing()
	{
		base.OnAppearing();
		_vm.GetUsers();
	}
}
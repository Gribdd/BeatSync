namespace BeatSync.Pages
{
    public partial class ViewProfile : ContentPage
    {
        CustomerLandingPageViewModel _vm;
        public ViewProfile(CustomerLandingPageViewModel vm)
        {
            BindingContext = _vm = vm;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _vm.GetActiveCustomer();
            _vm.GetPlaylists();
        }   
    }
}

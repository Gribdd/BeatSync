namespace BeatSync.Pages
{
    public partial class ViewProfile : ContentPage
    {
        public ViewProfile(CustomerLandingPageViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}

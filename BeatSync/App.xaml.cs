using BeatSync.Models;
using BeatSync.Views;

namespace BeatSync
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}

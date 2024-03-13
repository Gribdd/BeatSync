using BeatSync.Pages;
using BeatSync.Services;
using BeatSync.ViewModel.Admin;
using BeatSync.Views;
using Microsoft.Extensions.Logging;

namespace BeatSync
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("Jua-Regular.ttf", "JuaRegular");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            //View
            builder.Services.AddTransient<AddArtist>();
            builder.Services.AddTransient<ArtistManagement>();
            builder.Services.AddTransient<AddSong>();
            builder.Services.AddTransient<SongManagement>();
            builder.Services.AddTransient<PublisherManagement>();
            builder.Services.AddTransient<AddPublisher>();
            builder.Services.AddTransient<Admin_LandingPage>();
            builder.Services.AddTransient<Admin_LoginPage>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<AddUser>();
            builder.Services.AddTransient<UserManagement>();
            builder.Services.AddTransient<LoginPage>(); 
            builder.Services.AddTransient<SignUpPage>();

            builder.Services.AddTransient<CreateAccountPassword>();
            builder.Services.AddTransient<CreateAccountDOB>();
            builder.Services.AddTransient<CreateAccountFirstName>();
            builder.Services.AddTransient<CreateAccountLastName>();
            builder.Services.AddTransient<CreateAccountGender>();

            builder.Services.AddTransient<CustomerLandingPage>();


            //Viewmodel
            builder.Services.AddTransient<AddArtistViewModel>();
            builder.Services.AddTransient<ArtistManagementViewModel>();
            builder.Services.AddTransient<AddSongViewModel>();
            builder.Services.AddTransient<SongManagementViewModel>();
            builder.Services.AddTransient<PublisherManagementViewModel >();
            builder.Services.AddTransient<AddPublisherViewModel >();
            builder.Services.AddTransient<AddUserViewModel>();
            builder.Services.AddTransient<UserManagementViewModel>();
            

            //Service
            builder.Services.AddTransient<AdminService>();

            return builder.Build();
        }
    }
}

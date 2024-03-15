using BeatSync.Pages;
using BeatSync.Services;
using BeatSync.ViewModel.Admin;
using BeatSync.ViewModel.LoginAndRegistration;
using BeatSync.ViewModel.PublisherShell;
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
                })
                .RegisterServices()
                .RegisterViewModels()
                .RegisterViews();
#if DEBUG
                builder.Logging.AddDebug();
#endif
            return builder.Build();

        }

        public static MauiAppBuilder RegisterServices(this MauiAppBuilder mauiAppBuilder)
            {
                mauiAppBuilder.Services.AddTransient<AdminService>();

                return mauiAppBuilder;
            }

            public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
            {
                //Viewmodel
                mauiAppBuilder.Services.AddTransient<AddArtistViewModel>();
                mauiAppBuilder.Services.AddTransient<ArtistManagementViewModel>();
                mauiAppBuilder.Services.AddTransient<AddSongViewModel>();
                mauiAppBuilder.Services.AddTransient<SongManagementViewModel>();
                mauiAppBuilder.Services.AddTransient<PublisherManagementViewModel>();
                mauiAppBuilder.Services.AddTransient<AddPublisherViewModel>();
                mauiAppBuilder.Services.AddTransient<AddUserViewModel>();
                mauiAppBuilder.Services.AddTransient<UserManagementViewModel>();

                //Viewmodel registration and Login
                mauiAppBuilder.Services.AddTransient<MainPageViewModel>();
                mauiAppBuilder.Services.AddTransient<SignUpPageViewModel>();
                mauiAppBuilder.Services.AddTransient<CreateAccountUsernameViewModel>();
                mauiAppBuilder.Services.AddTransient<CreateAccountPasswordViewModel>();
                mauiAppBuilder.Services.AddTransient<CreateAccountDOBViewModel>();
                mauiAppBuilder.Services.AddTransient<CreateAccountFirstNameViewModel>();
                mauiAppBuilder.Services.AddTransient<CreateAccountLastNameViewModel>();
                mauiAppBuilder.Services.AddTransient<CreateAccountGenderViewModel>();
                mauiAppBuilder.Services.AddTransient<CreateAccountUploadImageViewModel>();
                
                //Publisher
                mauiAppBuilder.Services.AddTransient<SongManagementPubViewModel>();
                mauiAppBuilder.Services.AddTransient<LibraryPageViewModel>();

                return mauiAppBuilder;
            }

            public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
            {
                //View
                mauiAppBuilder.Services.AddTransient<AddArtist>();
                mauiAppBuilder.Services.AddTransient<ArtistManagement>();
                mauiAppBuilder.Services.AddTransient<AddSong>();
                mauiAppBuilder.Services.AddTransient<SongManagement>();
                mauiAppBuilder.Services.AddTransient<PublisherManagement>();
                mauiAppBuilder.Services.AddTransient<AddPublisher>();
                mauiAppBuilder.Services.AddTransient<Admin_LandingPage>();
                mauiAppBuilder.Services.AddTransient<Admin_LoginPage>();
                mauiAppBuilder.Services.AddTransient<MainPage>();
                mauiAppBuilder.Services.AddTransient<AddUser>();
                mauiAppBuilder.Services.AddTransient<UserManagement>();

                //View Signup and Login
                mauiAppBuilder.Services.AddTransient<LoginPage>();
                mauiAppBuilder.Services.AddTransient<SignUpPage>();
                mauiAppBuilder.Services.AddTransient<CreateAccountUsername>();
                mauiAppBuilder.Services.AddTransient<CreateAccountPassword>();
                mauiAppBuilder.Services.AddTransient<CreateAccountDOB>();
                mauiAppBuilder.Services.AddTransient<CreateAccountFirstName>();
                mauiAppBuilder.Services.AddTransient<CreateAccountLastName>();
                mauiAppBuilder.Services.AddTransient<CreateAccountGender>();
                mauiAppBuilder.Services.AddTransient<CreateAccountUploadImage>();
                mauiAppBuilder.Services.AddTransient<CustomerLandingPage>();

                //Publisher
                mauiAppBuilder.Services.AddTransient<LibraryPage>();
                mauiAppBuilder.Services.AddTransient<SongManagementPub>();



                return mauiAppBuilder;
            }
    }
}

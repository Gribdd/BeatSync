using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;

namespace BeatSync
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkitMediaElement()
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
            mauiAppBuilder.Services.AddTransient<UserAuthService>();
            mauiAppBuilder.Services.AddTransient<UserValidationService>();
            mauiAppBuilder.Services.AddTransient<UserService>();
            mauiAppBuilder.Services.AddTransient<SongService>();
            mauiAppBuilder.Services.AddTransient<AlbumService>();
            mauiAppBuilder.Services.AddTransient<PublisherService>();
            mauiAppBuilder.Services.AddTransient<ArtistService>();
            mauiAppBuilder.Services.AddTransient<PlaylistService>();
            mauiAppBuilder.Services.AddTransient<FileUploadService>();

            return mauiAppBuilder;
        }

        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
        {
            //Viewmodel
            mauiAppBuilder.Services.AddTransient<AddArtistViewModel>();
            mauiAppBuilder.Services.AddTransient<ArtistManagementViewModel>();
            mauiAppBuilder.Services.AddTransient<SongManagementViewModel>();
            mauiAppBuilder.Services.AddTransient<PublisherManagementViewModel>();
            mauiAppBuilder.Services.AddTransient<AddPublisherViewModel>();
            mauiAppBuilder.Services.AddTransient<AddUserViewModel>();
            mauiAppBuilder.Services.AddTransient<UserManagementViewModel>();

            //Viewmodel registration and Login
            mauiAppBuilder.Services.AddTransient<MainPageViewModel>();
            mauiAppBuilder.Services.AddTransient<SignUpPageViewModel>();
            mauiAppBuilder.Services.AddTransient<LoginPageViewModel>();
            mauiAppBuilder.Services.AddTransient<CreateAccountUsernameViewModel>();
            mauiAppBuilder.Services.AddTransient<CreateAccountPasswordViewModel>();
            mauiAppBuilder.Services.AddTransient<CreateAccountDOBViewModel>();
            mauiAppBuilder.Services.AddTransient<CreateAccountFirstNameViewModel>();
            mauiAppBuilder.Services.AddTransient<CreateAccountLastNameViewModel>();
            mauiAppBuilder.Services.AddTransient<CreateAccountGenderViewModel>();
            mauiAppBuilder.Services.AddTransient<CreateAccountUploadImageViewModel>();

            //General
            mauiAppBuilder.Services.AddTransient<AddAlbumSongsViewModel>();
            mauiAppBuilder.Services.AddTransient<AddSongViewModel>();
            
            //Publisher
            mauiAppBuilder.Services.AddTransient<SongManagementPubViewModel>();
            mauiAppBuilder.Services.AddTransient<LibraryPageViewModel>();
            mauiAppBuilder.Services.AddTransient<AddAlbumPublisherViewModel>();

            //Users
            mauiAppBuilder.Services.AddTransient<UserHistoryViewModel>();
            mauiAppBuilder.Services.AddTransient<SearchPageViewModel>();
            mauiAppBuilder.Services.AddTransient<LandingPageViewModel>();
            mauiAppBuilder.Services.AddTransient<CustomerLibraryPageViewModel>();
            mauiAppBuilder.Services.AddTransient<AddPlaylistCustomerViewModel>();
            mauiAppBuilder.Services.AddTransient<AddPlaylistSongsCustomerViewModel>();

            return mauiAppBuilder;
        }

        public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
        {
            //View
            mauiAppBuilder.Services.AddTransient<AddArtist>();
            mauiAppBuilder.Services.AddTransient<ArtistManagement>();
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
            
            //General, meaning can be used more than once
            mauiAppBuilder.Services.AddTransient<AddAlbumSongs>();
            mauiAppBuilder.Services.AddTransient<AddSong>();
            

            //Publisher
            mauiAppBuilder.Services.AddTransient<LibraryPage>();
            mauiAppBuilder.Services.AddTransient<SongManagementPub>();
            mauiAppBuilder.Services.AddTransient<AddAlbumPublisher>();

            //Users
            mauiAppBuilder.Services.AddTransient<UserHistory>();
            mauiAppBuilder.Services.AddTransient<SearchPage>();
            mauiAppBuilder.Services.AddTransient<LandingPage>();
            mauiAppBuilder.Services.AddTransient<CustomerLibraryPage>();
            mauiAppBuilder.Services.AddTransient<AddPlaylistCustomer>();
            mauiAppBuilder.Services.AddTransient<AddPlaylistSongsCustomer>();


            return mauiAppBuilder;
        }
    }
}
